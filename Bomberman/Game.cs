using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bomberman
{
    class Game
    {
        PictureBox pictureBox;
        Map map;
        Display display;
        System.Threading.Timer timer;

        public Game(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            map = new Map(pictureBox.Width, pictureBox.Height);
            display = new Display(pictureBox);
            display.init();
            display.draw(map.getGameObjects());
            timer = new System.Threading.Timer(tic, null, 0, 200);
        }

        private void tic(object obj)
        {
            if (map.tic())
            {
                display.remove(map.needUpdate);
                display.draw(map.needUpdate);
                map.needUpdate.Clear();
            }
        }
    }
}
