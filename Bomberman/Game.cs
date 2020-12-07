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
        Map map;
        Display display;
        System.Windows.Forms.Timer timer;
        Bomberman bomberman1, bomberman2;

        public Game(PictureBox pictureBox)
        {
            map = new Map();
            display = new Display(pictureBox);
            display.draw(map.getGameObjects());
            bomberman1 = new Bomberman(new Point(50, 50), Keys.Up, Keys.Down, Keys.Right, Keys.Left, Keys.Space, "Player1",
                Properties.Resources.bomberman_green_up, Properties.Resources.bomberman_green_down, Properties.Resources.bomberman_green_right, Properties.Resources.bomberman_green_left, Properties.Resources.bomberman_green_stop);
            map.addBomberman(bomberman1);
            bomberman2 = new Bomberman(new Point(850, 550),/* Keys.W, Keys.S, Keys.D, Keys.A, Keys.C, */ "Player2", 
                Properties.Resources.bomberman_red_up, Properties.Resources.bomberman_red_down, Properties.Resources.bomberman_red_right, Properties.Resources.bomberman_red_left, Properties.Resources.bomberman_red_stop);
            map.addBomberman(bomberman2);
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
            display.draw(map.getBonuses());
            display.draw(map.getFlame());
            display.update();
            if (!map.gameIsOn) timer.Enabled = false;
            
        }
    }
}
