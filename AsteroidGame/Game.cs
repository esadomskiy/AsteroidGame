using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {

        }

        public static void Init(Form form)
        {
            //графическое устройство для вывода графики
            Graphics g;
            // доступ к графическому контексту буферу
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            //создаем объект (повернхность рисования) и связываем его с формой
            //запоминаем размеры форм
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            //связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            //Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();

            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet)) 
                {
                    System.Media.SystemSounds.Hand.Play();
                }
            }
            _bullet.Update();
        }

        private static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

        static int GetRandomStartPosX()
        {
            return r.Next(Game.Width, Game.Width * 2);
        }
        static int GetRandomStartPosY()
        {
            return r.Next(0, Game.Height);
        }

        static Random r = new Random();

        public static void Load()
        {
            _objs = new BaseObject[55];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[40];

            for (int i = 0; i < 55; i++)
                _objs[i] = new Star(new Point(GetRandomStartPosX(), GetRandomStartPosY()), new Point(i, 0), new Size(2, 2));

            for (int i = 0; i < 40; i++)
                _asteroids[i] = new Asteroid(new Point(GetRandomStartPosX(), GetRandomStartPosY()), new Point(i, 0), new Size(30, 20));

            //var rnd = new Random();
            //for (var i = 0; i < _objs.Length; i++)
            //{
            //    int r = rnd.Next(5, 50);
            //    _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            //}
            //for (var i = 0; i < _asteroids.Length; i++)
            //{
            //    int r = rnd.Next(5, 50);
            //    _asteroids[i] = new Asteroid(new Point(100, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            //}

        }

    }
}
