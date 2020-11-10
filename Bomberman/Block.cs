using System.Drawing;

namespace Bomberman
{
    class Block : GameObjectIntr
    {
        Point coords;
        Size size;
        Bitmap texture;

        public Point getCoords()
        {
            return coords;
        }

        public Size getSize()
        {
            return size;
        }

        public Bitmap getTexture()
        {
            return texture;
        }
    }
}
