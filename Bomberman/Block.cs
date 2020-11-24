using System.Drawing;

namespace Bomberman
{
    class Block : Template
    {
        static Bitmap texture1;
        static Bitmap texture2;
        public bool breakable;

        static Block()
        {
            texture1 = new Bitmap(50, 50);
            texture2 = new Bitmap(50, 50);
            using (var g = Graphics.FromImage(texture1))
            {
                g.Clear(Color.Gray);
            }
            using (var g = Graphics.FromImage(texture2))
            {
                g.Clear(Color.DarkSlateGray);
            }
        }

        public Block(Point coord, bool breakable)
        {
            this.breakable = breakable;
            //test
            coords = coord;
            size = new Size(50, 50);
            //test
        }

        public override Bitmap getTexture()
        {
            if (breakable) return texture1;
            else return texture2;
        }
    }
}
