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
    class Game
    {
        PictureBox pictureBox;
        Map map;
        Display display;
        System.Threading.Timer timer;
        //test
        Bomberman b, b2;
        //test

        public Game(PictureBox pictureBox)
        {
            
            this.pictureBox = pictureBox;
            map = new Map(pictureBox.Width, pictureBox.Height);
            display = new Display(pictureBox);
            display.init();
            display.draw(map.getGameObjects());
            //test
            b = new Bomberman(new Point(70, 70), Keys.Up, Keys.Down, Keys.Right, Keys.Left);
            display.draw(b);
            map.addBomberman(b);
            b2 = new Bomberman(new Point(200, 200), Keys.W, Keys.S, Keys.D, Keys.A);
            display.draw(b2);
            map.addBomberman(b2);
            //test
            timer = new System.Threading.Timer(tic, null, 0, 20);
        }

        public void keyDown(Keys e)
        {
            map.keyDown(e);
        }

        public void keyUp(Keys e)
        {
            map.keyUp(e);
        }

        private void tic(object obj)
        {
            if (map.tic())
            {
                display.remove(map.needUpdate);
                map.doActions();
                display.draw(map.needUpdate);
                map.needUpdate.Clear();
            }
        }
    }
}
