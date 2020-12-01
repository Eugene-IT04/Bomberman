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
            texture1 = Properties.Resources.block_standart_texture2;
            texture2 = Properties.Resources.block_unbreakable_texture;
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
