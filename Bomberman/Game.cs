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
        System.Windows.Forms.Timer timer;
        object lock_obj = new object();
        //test
        Bomberman b, b2, b3, b4, b5, b6, b7, b8;
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
            b3 = new Bomberman(new Point(220, 200), Keys.W, Keys.S, Keys.D, Keys.A);
            b4 = new Bomberman(new Point(200, 220), Keys.W, Keys.S, Keys.D, Keys.A);
            b5 = new Bomberman(new Point(240, 200), Keys.W, Keys.S, Keys.D, Keys.A);
            b6 = new Bomberman(new Point(200, 240), Keys.W, Keys.S, Keys.D, Keys.A);
            b7 = new Bomberman(new Point(260, 200), Keys.W, Keys.S, Keys.D, Keys.A);
            b8 = new Bomberman(new Point(200, 260), Keys.W, Keys.S, Keys.D, Keys.A);
            display.draw(b2);
            map.addBomberman(b2);
            map.addBomberman(b3);
            map.addBomberman(b4);
            map.addBomberman(b5);
            map.addBomberman(b6);
            map.addBomberman(b7);
            map.addBomberman(b8);
            //test
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 2;
            timer.Tick += tic;
            timer.Enabled = true;
        }

        public void keyDown(Keys e)
        {
            map.keyDown(e);
        }

        public void keyUp(Keys e)
        {
            map.keyUp(e);
        }

        private void tic(object sender, EventArgs e)
        {
            map.tic();
            map.doActions();
            display.draw(map.getGameObjects());
            display.draw(map.getBombermans());
            display.update();
        }
    }
}
