using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Bomberman
{
    class Map
    {
        List<GameObjectIntr> gameObjects;
        List<Bomberman> bombermans;
        public List<GameObjectIntr> needUpdate;
        int width, height;
        PointF moveUp = new PointF(0, -1), moveDown = new PointF(0, 1), moveLeft = new PointF(-1, 0), moveRight = new PointF(1, 0), stop = new PointF(0, 0);

        public Map(int width, int height)
        {
            gameObjects = new List<GameObjectIntr>();
            bombermans = new List<Bomberman>();
            needUpdate = new List<GameObjectIntr>();
            this.width = width;
            this.height = height;
            fill();
        }

        public void addBomberman(Bomberman bomberman)
        {
            bombermans.Add(bomberman);
        }

        public List<Bomberman> getBombermans()
        {
            return bombermans;
        }

        private PointF checkCollisions(Bomberman b, PointF moveVector)
        {
            PointF res = moveVector;
            PointF current;
            for(int i = 0; i < gameObjects.Count; i++)
            {
                current = b.checkColl(gameObjects[i], moveVector);
                if (Math.Abs(res.X) > current.X) res.X = current.X;
                if (Math.Abs(res.Y) > current.Y) res.Y = current.Y;
            }
            return res;
        }

        private PointF multiplyVector(PointF vector, float number)
        {
            return new PointF(vector.X * number, vector.Y * number);
        }

        public bool tic()
        {
            bool res = false;
            for(int i = 0; i < bombermans.Count; i++)
            {
                var b = bombermans[i];
                if (b.direction == Directions.up)
                {
                    b.moveVector = checkCollisions(b, multiplyVector(moveUp, b.speed));
                }
                else if (b.direction == Directions.down)
                {
                    b.moveVector = checkCollisions(b, multiplyVector(moveDown, b.speed));
                }
                else if (b.direction == Directions.right)
                {
                    b.moveVector = checkCollisions(b, multiplyVector(moveRight, b.speed));
                }
                else if (b.direction == Directions.left)
                {
                    b.moveVector = checkCollisions(b, multiplyVector(moveLeft, b.speed));
                }
                else if(b.direction == Directions.stop)
                {
                    b.moveVector = stop;
                }
            }
            return res;
        }

        public void doActions()
        {
            foreach (var b in bombermans) b.move();
        }

        public List<GameObjectIntr> getGameObjects()
        {
            return gameObjects;
        }

        private void fill()
        {
            gameObjects.Add(new Block(new System.Drawing.Point(10, 10), false));
        }

        public void keyDown(Keys key)
        {
            foreach(var b in bombermans)
            {
                if (key == b.upKey) b.direction = Directions.up;
                else if (key == b.downKey) b.direction = Directions.down;
                else if (key == b.leftKey) b.direction = Directions.left;
                else if (key == b.rightKey) b.direction = Directions.right;
            }
        }
        
        public void keyUp(Keys key)
        {
            foreach (var b in bombermans)
            {
                if (key == b.upKey && b.direction == Directions.up) b.direction = Directions.stop;
                else if (key == b.downKey && b.direction == Directions.down) b.direction = Directions.stop;
                else if (key == b.rightKey && b.direction == Directions.right) b.direction = Directions.stop;
                else if (key == b.leftKey && b.direction == Directions.left) b.direction = Directions.stop;
            }
        }
    }
}
