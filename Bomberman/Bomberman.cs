using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bomberman
{
    class Bomberman : Template
    {
        Bitmap texture_up, texture_down, texture_left, texture_right, texture_stop;
        public Keys upKey;
        public Keys downKey;
        public Keys rightKey;
        public Keys leftKey;
        public Keys plantBombKey;
        public float speed = 3f;
        public PointF moveVector;
        public Directions direction;
        public int maxBombsCount = 1;
        public int currentBombCount = 0;
        public int bombPower = 1;
        public String name;
        public bool bombPlanted = false;
        public int[,] objMap;
        private bool isBot;

        public Bomberman(Point startPoint, Keys upKey, Keys downKey, Keys rightKey, Keys leftKey, Keys plantBombKey, String name, Bitmap texture_up, Bitmap texture_down, Bitmap texture_right, Bitmap texture_left, Bitmap texture_stop)
        {
            isBot = false;
            this.texture_down = texture_down;
            this.texture_left = texture_left;
            this.texture_up = texture_up;
            this.texture_right = texture_right;
            this.texture_stop = texture_stop;
            this.name = name;
            direction = Directions.stop;
            this.coords = startPoint;
            this.upKey = upKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.leftKey = leftKey;
            this.plantBombKey = plantBombKey;
            size = new Size(45, 45);
        }

        public Bomberman(Point startPoint, String name, Bitmap texture_up, Bitmap texture_down, Bitmap texture_right, Bitmap texture_left, Bitmap texture_stop)
        {
            isBot = true;
            this.texture_down = texture_down;
            this.texture_left = texture_left;
            this.texture_up = texture_up;
            this.texture_right = texture_right;
            this.texture_stop = texture_stop;
            this.name = name;
            direction = Directions.stop;
            this.coords = startPoint;
            size = new Size(45, 45);
        }

        public void tic()
        {
            if (isBot)
            {
                direction = Directions.up;
            }
        }

        public override Bitmap getTexture()
        {
            if (direction == Directions.up) return texture_up;
            else if (direction == Directions.down) return texture_down;
            else if (direction == Directions.right) return texture_right;
            else if (direction == Directions.left) return texture_left;
            else return texture_stop;
        }

        public void move()
        {
            coords.X += moveVector.X;
            coords.Y += moveVector.Y;
        }
        public Bomb plantBomb() {
            currentBombCount++;
            return new Bomb(new PointF((float)Math.Round((double)coords.X / 50) * 50, (float)Math.Round((double)coords.Y / 50) * 50), name, bombPower); 
        }

    }
}
