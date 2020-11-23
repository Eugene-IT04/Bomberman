using System.Drawing;

interface GameObjectIntr
{
    PointF getCoords();
    Bitmap getTexture();
    Size getSize();
    bool checkColl(GameObjectIntr gameObject);
    PointF checkColl(GameObjectIntr gameObject, PointF moveVector);
}