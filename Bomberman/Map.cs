using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bomberman
{
    class Map
    {
        List<GameObjectIntr> gameObjects;
        List<Bomberman> bombermans;
        public List<GameObjectIntr> needUpdate;
        int width, height;

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

        public bool tic()
        {
            bool res = false;
            foreach(var b in bombermans)
            {
                if (b.direction == Directions.up)
                {
                    b.action = new Action(b.goUp);
                    res = true;
                    needUpdate.Add(b);
                }
                else if (b.direction == Directions.down)
                {
                    b.action = new Action(b.goDown);
                    res = true;
                    needUpdate.Add(b);
                }
                else if (b.direction == Directions.right)
                {
                    b.action = new Action(b.goRight);
                    res = true;
                    needUpdate.Add(b);
                }
                else if (b.direction == Directions.left)
                {
                    b.action = new Action(b.goLeft);
                    res = true;
                    needUpdate.Add(b);
                }
                else if(b.direction == Directions.stop)
                {
                    b.action = new Action(() => { });
                }
            }
            return res;
        }

        public void doActions()
        {
            foreach (var b in bombermans) b.action();
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
