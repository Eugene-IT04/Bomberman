using System.Drawing;

namespace Bomberman
{
    class Block : Template
    {
        static Bitmap texture;
        bool breakable;

        public Block(Point coord, bool breakable)
        {
            this.breakable = breakable;
            //test
            coords = coord;
            size = new Size(50, 50);
            texture = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(texture))
                g.Clear(Color.Black);
            //test
        }

        public override Bitmap getTexture()
        {
            return texture;
        }
    }
}
