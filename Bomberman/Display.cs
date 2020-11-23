using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    class Display : DisplayIntrf
    {
        private object lockObject = new object();
        PictureBox pb;
        Bitmap bt;
        Graphics g;
        public Display(PictureBox pb)
        {
            this.pb = pb;
            bt = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bt);
        }

        public void init()
        {
            //test
            g.Clear(Color.AliceBlue);
            //test
        }

        public void update()
        {
            pb.Image = bt;
            pb.Refresh();
            g.Clear(Color.Aqua);
        }

        public void draw(List<GameObjectIntr> gameObjects)
        {
            foreach (var gameObject in gameObjects) dr(gameObject);
        }

        public void draw(List<Bomberman> gameObjects)
        {
            foreach (var gameObject in gameObjects) dr(gameObject);
        }

        public void draw(GameObjectIntr gameObject)
        {
            dr(gameObject);
        }

        public void remove(List<GameObjectIntr> gameObjects)
        {
            foreach (var gameObject in gameObjects) rm(gameObject);
            lock (lockObject)
            {
                pb.Image = bt;
            }

        }

        public void remove(GameObjectIntr gameObject)
        {
            rm(gameObject);
            lock (lockObject)
            {
                pb.Image = bt;
            }
        }

        private void rm(GameObjectIntr gameObject)
        {
            //test
            lock (lockObject)
            {
                g.FillRectangle(Brushes.AliceBlue, new RectangleF(gameObject.getCoords(), gameObject.getSize()));
            }
            //test
        }

        private void dr(GameObjectIntr gameObject)
        {
                g.DrawImage(gameObject.getTexture(), gameObject.getCoords());
        }
    }
}
