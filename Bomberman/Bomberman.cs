using System.Drawing;
using System.Windows.Forms;

namespace Bomberman
{
    class Bomberman : GameObjectIntr, Controlled
    {
        Point coords;
        Size size;
        Bitmap texture;
        public Keys upKey;
        public Keys downKey;
        public Keys rightKey;
        public Keys leftKey;
        public Directions direction = Directions.stop;
        public Action action;
        int step = 2;

        public Bomberman(Point startPoint, Keys upKey, Keys downKey, Keys rightKey, Keys leftKey)
        {
            action = new Action(() => { });
            this.coords = startPoint;
            this.upKey = upKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.leftKey = leftKey;
            //test
            size = new Size(50, 50);
            texture = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(texture))
                g.Clear(Color.Green);
            //test
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

        public void goUp() { coords.Y -= step; }
        public void goDown() { coords.Y += step; }
        public void goRight() { coords.X += step; }
        public void goLeft() { coords.X -= step; }
        public void plantBomb() { }

    }
    public delegate void Action();
}
