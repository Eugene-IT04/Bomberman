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

        public bool tic()
        {
            foreach(var b in bombermans)
            {
                if (b.direction == Directions.up) b.goUp();
            }
            return false;
        }

        public List<GameObjectIntr> getGameObjects()
        {
            return gameObjects;
        }

        private void fill()
        {
            gameObjects.Add(new Block(new System.Drawing.Point(10, 10), false));
        }

        public void keyDown(KeyEventArgs key)
        {
            foreach(var b in bombermans)
            {
                if (key == b.upKey) b.direction = Directions.up;
                else if (key == b.downKey) b.direction = Directions.down;
                else if (key == b.leftKey) b.direction = Directions.left;
                else if (key == b.rightKey) b.direction = Directions.right;
            }
        }
        
        public void keyUp(KeyEventArgs key)
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
