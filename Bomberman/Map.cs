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
        int w = 19, h = 13;
        int[,] objMap;
        List<GameObjectIntr> gameObjects;
        List<Bomberman> bombermans;
        List<Bomb> bombs;
        List<Flame> flames;
        List<Bonus> bonuses;
        public bool gameIsOn = true;
        PointF moveUp = new PointF(0, -1), moveDown = new PointF(0, 1), moveLeft = new PointF(-1, 0), moveRight = new PointF(1, 0), stop = new PointF(0, 0);
        public Map()
        {
            objMap = new int[w, h];
            gameObjects = new List<GameObjectIntr>();
            bombermans = new List<Bomberman>();
            bombs = new List<Bomb>();
            flames = new List<Flame>();
            bonuses = new List<Bonus>();
            fill();
        }

        public void addBomberman(Bomberman bomberman)
        {
            bomberman.objMap = objMap;
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

        public List<Bonus> getBonuses()
        {
            return bonuses;
        }

        private PointF checkCollisions(Bomberman b, PointF moveVector)
        {
            PointF res = moveVector;
            PointF current;
            for(int i = 0; i < bonuses.Count; i++)
            {
                if (b.checkColl(bonuses[i]))
                {
                    bonuses[i].improveBomberman(b);
                    bonuses.RemoveAt(i);
                }
            }
            for(int i = 0; i < gameObjects.Count; i++)
            {
                current = b.checkColl(gameObjects[i], moveVector);
                if (Math.Abs(res.X) > Math.Abs(current.X) && moveVector.X != 0) res.X = current.X;
                if (Math.Abs(res.Y) > Math.Abs(current.Y) && moveVector.Y != 0) res.Y = current.Y;
                if (moveVector.X == 0 && current.X != 0) res.X = current.X;
                if (moveVector.Y == 0 && current.Y != 0) res.Y = current.Y;
            }
            for(int i = 0; i < bombs.Count; i++)
            {
                if (!bombs[i].checkColl(b))
                {
                    current = b.checkColl(bombs[i], moveVector);
                    if (Math.Abs(res.X) > Math.Abs(current.X) && moveVector.X != 0) res.X = current.X;
                    if (Math.Abs(res.Y) > Math.Abs(current.Y) && moveVector.Y != 0) res.Y = current.Y;
                    if (moveVector.X == 0 && current.X != 0) res.X = current.X;
                    if (moveVector.Y == 0 && current.Y != 0) res.Y = current.Y;
                }
            }
            return res;
        }

        private PointF multiplyVector(PointF vector, float number)
        {
            return new PointF(vector.X * number, vector.Y * number);
        }

        private void bombermansTic()
        {
            foreach(var b in bombermans)
            {
                b.tic();
                if (b.bombPlanted && b.currentBombCount < b.maxBombsCount)
                {
                    b.bombPlanted = false;
                    Bomb bomb = b.plantBomb();
                    bombs.Add(bomb);
                    objMap[(int)bomb.getCoords().X / 50, (int)bomb.getCoords().Y / 50] = 5;
                }
            }
        }

        private void bombTic()
        {
            for(int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i].exploded())
                {
                    foreach (var b in bombermans)
                    {
                        if (b.name.Equals(bombs[i].owner))
                        {
                            b.currentBombCount--;
                            break;
                        }
                    }
                    objMap[(int)bombs[i].getCoords().X / 50, (int)bombs[i].getCoords().Y / 50] = 0;
                    flames.AddRange(bombs[i].getFlames());
                    bombs.RemoveAt(i);
                    i--;
                }
            }
        }

        private void processFlames()
        {
            Flame nextFlame;
            Random r = new Random();
            for(int i = 0; i < flames.Count; i++)
            {
                if (flames[i].active)
                {
                    for (int j = 0; j < gameObjects.Count; j++)
                    {
                        if (flames[i].checkColl(gameObjects[j]) && flames[i].active)
                        {
                            Block bl = (Block)gameObjects[j];
                            if (bl.breakable)
                            {
                                if(r.Next(100) < 20)
                                {
                                    bonuses.Add(new Bonus(gameObjects[j].getCoords()));
                                    objMap[(int)gameObjects[j].getCoords().X / 50, (int)gameObjects[j].getCoords().Y / 50] = 10;
                                }
                                else objMap[(int)gameObjects[j].getCoords().X / 50, (int)gameObjects[j].getCoords().Y / 50] = 0;
                                gameObjects.RemoveAt(j);
                            }
                            else flames[i].tics = 0;
                            flames[i].power = 0;
                            flames[i].active = false;
                            break;
                        }
                    }
                    if (flames[i].active)
                    {
                        for (int j = 0; j < bombermans.Count; j++)
                        {
                            if (flames[i].checkColl(bombermans[j]))
                            {
                                bombermans.RemoveAt(j);
                                checkForWinner();
                                if (!gameIsOn) return;
                                flames[i].power = 0;
                                flames[i].active = false;
                                break;
                            }
                        }
                        for (int j = 0; j < bonuses.Count; j++)
                        {
                            if (flames[i].checkColl(bonuses[j]))
                            {
                                objMap[(int)bonuses[j].getCoords().X / 50, (int)bonuses[j].getCoords().Y / 50] = 0;
                                bonuses.RemoveAt(j);
                                flames[i].power = 0;
                                flames[i].active = false;
                                break;
                            }
                        }
                        nextFlame = flames[i].spread();
                        if (nextFlame != null) flames.Add(nextFlame);
                    }
                }
            }
            clearFlames();
        }

        public void clearFlames()
        {
            for(int i = 0; i < flames.Count; i++)
            {
                flames[i].active = false;
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
            bombermansTic();
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
            bool cont;
            for(int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    cont = (i == 1 && j == 1) || (i == 1 && j == 2) || (i == 2 && j == 1) || (i == 17 && j == 11) || (i == 17 && j == 10) || (i == 16 && j == 11);
                    if (cont)
                    {
                        continue;
                    }
                    if (((i + j) % 2 == 0 && i % 2 == 0) || i == 0 || i == 18 || j == 0 || j == 12)
                    {
                        gameObjects.Add(new Block(new Point(50 * i, 50 * j), false));
                        objMap[i, j] = 2;
                    }
                    else
                    {
                        gameObjects.Add(new Block(new Point(50 * i, 50 * j), true));
                        objMap[i, j] = 1;
                    }
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
                else if (key == b.plantBombKey)
                {
                    if (b.currentBombCount < b.maxBombsCount)
                    {
                        Bomb bomb = b.plantBomb();
                        bombs.Add(bomb);
                        objMap[(int)bomb.getCoords().X / 50, (int)bomb.getCoords().Y / 50] = 5;
                    }
                }
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

        public void checkForWinner()
        {
            if (bombermans.Count == 1)
            {
                gameIsOn = false;
                MessageBox.Show(bombermans[0].name + " won!");
            }
        }
    }
}
