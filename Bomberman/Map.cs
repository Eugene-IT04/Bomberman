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
        List<Bomb> bombs;
        List<Flame> flames;
        int width, height;
        PointF moveUp = new PointF(0, -1), moveDown = new PointF(0, 1), moveLeft = new PointF(-1, 0), moveRight = new PointF(1, 0), stop = new PointF(0, 0);

        public Map(int width, int height)
        {
            gameObjects = new List<GameObjectIntr>();
            bombermans = new List<Bomberman>();
            bombs = new List<Bomb>();
            flames = new List<Flame>();
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

        public List<Bomb> getBombs()
        {
            return bombs;
        }

        private PointF checkCollisions(Bomberman b, PointF moveVector)
        {
            PointF res = moveVector;
            PointF current;
            for(int i = 0; i < gameObjects.Count; i++)
            {
                current = b.checkColl(gameObjects[i], moveVector);
                if (Math.Abs(res.X) > Math.Abs(current.X)) res.X = current.X;
                if (Math.Abs(res.Y) > Math.Abs(current.Y)) res.Y = current.Y;
            }
            return res;
        }

        private PointF multiplyVector(PointF vector, float number)
        {
            return new PointF(vector.X * number, vector.Y * number);
        }

        private void bombTic()
        {
            for(int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i].exploded())
                {
                    flames.AddRange(bombs[i].getFlames());
                    bombs.RemoveAt(i);
                    i--;
                }
            }
        }

        private void processFlames()
        {
            Flame nextFlame;
            for(int i = 0; i < flames.Count; i++)
            {
                if (flames[i].active)
                {
                    for (int j = 0; j < gameObjects.Count; j++)
                    {
                        if (flames[i].checkColl(gameObjects[j]))
                        {
                            Block bl = (Block)gameObjects[j];
                            if (bl.breakable) gameObjects.RemoveAt(j);
                            else flames[i].tics = 0;
                            flames[i].power = 0;
                            flames[i].active = false;
                            break;
                        }
                    }
                    for (int j = 0; j < bombermans.Count; j++)
                    {
                        if (flames[i].active && flames[i].checkColl(bombermans[j]))
                        {
                            bombermans.RemoveAt(j);
                            flames[i].power = 0;
                            flames[i].active = false;
                            break;
                        }
                    }
                    nextFlame = flames[i].spread();
                    if (nextFlame != null) flames.Add(nextFlame);
                }
            }
            clearFlames();
        }

        public void clearFlames()
        {
            for(int i = 0; i < flames.Count; i++)
            {
                flames[i].tics--;
                if (flames[i].tics <= 0)
                {
                    flames.RemoveAt(i);
                    i--;
                }
            }
        }

        public List<Flame> getFlame()
        {
            return flames;
        }

        public void tic()
        {
            bombTic();
            processFlames();
            for (int i = 0; i < bombermans.Count; i++)
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
            bool cont = false;
            for(int i = 0; i < 16; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    cont = (i == 1 && j == 1) || (i == 1 && j == 2) || (i == 2 && j == 1) || (i == 13 && j == 7) || (i == 13 && j == 6) || (i == 12 && j == 7);
                    if (cont)
                    {
                        cont = false;
                        continue;
                    }
                    if((i + j) % 2 == 0 && i % 2 == 0) gameObjects.Add(new Block(new Point(50 * i + i, 50 * j + j), false));
                    else gameObjects.Add(new Block(new Point(50 * i + i, 50 * j + j), true));
                }
            }
        }

        public void keyDown(Keys key)
        {
            foreach(var b in bombermans)
            {
                if (key == b.upKey) b.direction = Directions.up;
                else if (key == b.downKey) b.direction = Directions.down;
                else if (key == b.leftKey) b.direction = Directions.left;
                else if (key == b.rightKey) b.direction = Directions.right;
                else if (key == b.plantBombKey) bombs.Add(b.plantBomb());
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
