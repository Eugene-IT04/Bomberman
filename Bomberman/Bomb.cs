using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Bomb : Template
    {
        static Bitmap texture;
        int tics = 170;
        int power = 1;
        public String owner;

        static Bomb()
        {
            texture = Properties.Resources.bomb_texture2;
        }

        public Bomb(PointF coords, String owner, int power)
        {
            this.coords = coords;
            size = new Size(50, 50);
            this.owner = owner;
            this.power = power;
        }

        public override Bitmap getTexture()
        {
            return texture;
        }

        public bool exploded()
        {
            tics--;
            if (tics <= 0) return true;
            return false;
        }

        public List<Flame> getFlames()
        {
            List<Flame> result = new List<Flame>();
            result.Add(new Flame(power, new PointF(coords.X + (size.Width / 2), coords.Y), Directions.up));
            result.Add(new Flame(power, new PointF(coords.X + (size.Width / 2), coords.Y + size.Height), Directions.down));
            result.Add(new Flame(power, new PointF(coords.X, coords.Y + (size.Height / 2)), Directions.left));
            result.Add(new Flame(power, new PointF(coords.X + size.Width, coords.Y + (size.Height / 2)), Directions.right));
            result.Add(new Flame(1, new PointF(coords.X + (size.Width / 2), coords.Y), Directions.down));
            result.Add(new Flame(1, new PointF(coords.X, coords.Y + (size.Height / 2)), Directions.right));
            return result;
        }
    }
}
