using System.Drawing;

namespace Bomberman
{
    class Bomberman : GameObjectIntr, Controlled
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

        public void goUp() { }
        public void goDown() { }
        public void goRight() { }
        public void goLeft() { }
        public void plantBomb() { }

    }
}
