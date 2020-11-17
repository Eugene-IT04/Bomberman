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
        Graphics g;
        public Display(PictureBox pb)
        {
            this.pb = pb;
            g = pb.CreateGraphics();
        }

        public void init()
        {
            //test
            g.Clear(Color.AliceBlue);
            //test
        }

        public void draw(List<GameObjectIntr> gameObjects)
        {
            foreach (var gameObject in gameObjects) draw(gameObject);
        }

        public void draw(GameObjectIntr gameObject)
        {
            g.DrawImage(gameObject.getTexture(), gameObject.getCoords());
        }

        public void remove(List<GameObjectIntr> gameObjects)
        {
            foreach (var gameObject in gameObjects) remove(gameObject);
        }

        public void remove(GameObjectIntr gameObject)
        {
            //test
            g.FillRectangle(Brushes.AliceBlue, new Rectangle(gameObject.getCoords(), gameObject.getSize()));
            //test
        }
    }
}
