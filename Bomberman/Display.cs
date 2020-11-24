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
            g.Clear(Color.LightCyan);
            //test
        }

        public void update()
        {
            pb.Image = bt;
            pb.Refresh();
            g.Clear(Color.LightCyan);
        }

        public void draw(List<GameObjectIntr> gameObjects)
        {
            foreach (var gameObject in gameObjects) dr(gameObject);
        }

        public void draw(List<Bomberman> gameObjects)
        {
            foreach (var gameObject in gameObjects) dr(gameObject);
        }

        public void draw(List<Bomb> gameObjects)
        {
            foreach (var gameObject in gameObjects) dr(gameObject);
        }

        public void draw(List<Flame> gameObjects)
        {
            foreach (var gameObject in gameObjects) dr(gameObject);
        }

        public void draw(GameObjectIntr gameObject)
        {
            dr(gameObject);
        }

        private void dr(GameObjectIntr gameObject)
        {
                g.DrawImage(gameObject.getTexture(), gameObject.getCoords());
        }
    }
}
