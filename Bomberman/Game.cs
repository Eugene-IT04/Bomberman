﻿using System;
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
            b = new Bomberman(new Point(50, 50), Keys.Up, Keys.Down, Keys.Right, Keys.Left, Keys.Space);
            map.addBomberman(b);
            b2 = new Bomberman(new Point(663, 357), Keys.W, Keys.S, Keys.D, Keys.A, Keys.C);
            map.addBomberman(b2);
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
            display.draw(map.getBombs());
            display.draw(map.getBombermans());
            display.draw(map.getFlame());
            //map.clearFlames();
            display.update();
        }
    }
}
