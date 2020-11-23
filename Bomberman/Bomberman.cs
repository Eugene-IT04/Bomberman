using System.Drawing;
using System.Windows.Forms;

namespace Bomberman
{
    class Bomberman : Template
    {
        Bitmap texture;
        public Keys upKey;
        public Keys downKey;
        public Keys rightKey;
        public Keys leftKey;
        public Directions direction = Directions.stop;
        public Action action;
        public float speed = 2.5f;
        public PointF moveVector;
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

        public override Bitmap getTexture()
        {
            return texture;
        }

        public void move()
        {
            coords.X += moveVector.X;
            coords.Y += moveVector.Y;
        }
        public void plantBomb() { }

    }
    public delegate void Action();
}
