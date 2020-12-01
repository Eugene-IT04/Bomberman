using System;
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
        public Keys plantBombKey;
        public float speed = 2.5f;
        public PointF moveVector;
        public Directions direction;

        public Bomberman(Point startPoint, Keys upKey, Keys downKey, Keys rightKey, Keys leftKey, Keys plantBombKey)
        {
            direction = Directions.stop;
            this.coords = startPoint;
            this.upKey = upKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.leftKey = leftKey;
            this.plantBombKey = plantBombKey;
            //test
            size = new Size(45, 45);
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
        public Bomb plantBomb() { return new Bomb(new PointF((float)Math.Round((double)coords.X / 50) * 50, (float)Math.Round((double)coords.Y / 50) * 50)); }

    }
}
