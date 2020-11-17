using System.Drawing;
using System.Windows.Forms;

namespace Bomberman
{
    class Bomberman : GameObjectIntr, Controlled
    {
        Point coords;
        Size size;
        Bitmap texture;
        public KeyEventArgs upKey;
        public KeyEventArgs downKey;
        public KeyEventArgs rightKey;
        public KeyEventArgs leftKey;
        public Directions direction = Directions.stop;

        public Bomberman(Point startPoint, KeyEventArgs upKey, KeyEventArgs downKey, KeyEventArgs rightKey, KeyEventArgs leftKey)
        {
            this.coords = startPoint;
            this.upKey = upKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.leftKey = leftKey;
        }

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

        public void goUp() { coords.Y--; }
        public void goDown() { coords.Y++; }
        public void goRight() { coords.X++; }
        public void goLeft() { coords.X--; }
        public void plantBomb() { }

    }
}
