using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Map
    {
        List<GameObjectIntr> gameObjects;
        List<Bomberman> bombermans;
        int width, height;

        public Map(int width, int height)
        {
            gameObjects = new List<GameObjectIntr>();
            bombermans = new List<Bomberman>();
            this.width = width;
            this.height = height;
            fill();
        }

        public List<GameObjectIntr> getGameObjects()
        {
            return gameObjects;
        }

        private void fill()
        {
            gameObjects.Add(new Block(new System.Drawing.Point(10, 10)));
        }

        
    }
}
