using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    abstract class Template : GameObjectIntr
    {
        protected PointF coords;
        protected Size size;

        public abstract Bitmap getTexture();

        public PointF getCoords()
        {
            return coords;
        }

        public Size getSize()
        {
            return size;
        }

        public bool checkColl(GameObjectIntr gameObject)
        {
            float x1 = coords.X, x2 = coords.X + size.Width, x3 = gameObject.getCoords().X, x4 = gameObject.getCoords().X + gameObject.getSize().Width;
            float y1 = coords.Y, y2 = coords.Y + size.Height, y3 = gameObject.getCoords().Y, y4 = gameObject.getCoords().Y + gameObject.getSize().Height;
            if (overlap(x1, x2, x3, x4) <= 0) return false;
            if (overlap(y1, y2, y3, y4) <= 0) return false;
            return true;
        }

        private float overlap(float x1, float x2, float x3, float x4)
        {
            return Math.Min(Math.Min(x1, x2), Math.Min(x3, x4)) + Math.Abs(x1 - x2) + Math.Abs(x3 - x4) - Math.Max(Math.Max(x1, x2), Math.Max(x3, x4));
        }

        public PointF checkColl(GameObjectIntr gameObject, PointF moveVector)
        {
            float onX, onY;
            float x1 = coords.X + moveVector.X, x2 = coords.X + size.Width + moveVector.X, x3 = gameObject.getCoords().X, x4 = gameObject.getCoords().X + gameObject.getSize().Width;
            float y1 = coords.Y + moveVector.Y, y2 = coords.Y + size.Height + moveVector.Y, y3 = gameObject.getCoords().Y, y4 = gameObject.getCoords().Y + gameObject.getSize().Height;
            onX = overlap(x1, x2, x3, x4);
            onY = overlap(y1, y2, y3, y4);
            if (onX <= 0 || onY <= 0) return moveVector;
        
            PointF rPoint = new PointF(0, 0);
            if (moveVector.X > 0) rPoint.X = moveVector.X - onX;
            else if (moveVector.X < 0) rPoint.X = moveVector.X + onX;
            if (moveVector.Y > 0) rPoint.Y = moveVector.Y - onY;
            else if (moveVector.Y < 0) rPoint.Y = moveVector.Y + onY;
            return rPoint;
        }
    }
}
