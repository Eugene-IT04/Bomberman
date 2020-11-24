using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Flame : Template
    {
        Bitmap texture;
        public int power;
        Directions direction;
        PointF nextFlame;
        float a = 50, b = 20;
        public bool active = true;
        public int tics = 5;

        public Flame(int power, PointF start, Directions direction)
        {
            this.power = power;
            this.direction = direction;
            if(direction == Directions.up)
            {
                coords = new PointF(start.X - (b / 2), start.Y - a);
                size = new Size((int)b, (int)a);
                nextFlame = new PointF(start.X, start.Y - a);
            }
            else if(direction == Directions.down)
            {
                coords = new PointF(start.X - (b / 2), start.Y);
                size = new Size((int)b, (int)a);
                nextFlame = new PointF(start.X, start.Y + a);
            }
            else if (direction == Directions.left)
            {
                coords = new PointF(start.X - a, start.Y - (b / 2));
                size = new Size((int)a, (int)b);
                nextFlame = new PointF(start.X - a, start.Y);
            }
            else if (direction == Directions.right)
            {
                coords = new PointF(start.X, start.Y - (b / 2));
                size = new Size((int)a, (int)b);
                nextFlame = new PointF(start.X + a, start.Y);
            }
            texture = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(texture))
                g.Clear(Color.Orange);
        }

        public Flame spread()
        {
            if (power <= 1) return null;
            return new Flame(power - 1, nextFlame, direction);
        }

        public override Bitmap getTexture()
        {
            return texture;
        }
    }
}
