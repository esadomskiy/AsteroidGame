﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidGame
{
    class Asteroid : BaseObject, ICloneable
    {
        public int Power { get; set; }
        Image image;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
            image = Image.FromFile(@"AsteroidImage.png");
        }

        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            asteroid.Power = Power;
            return asteroid;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }

        public override void Update()
        {
            Pos.X = Pos.X - 20; //Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
