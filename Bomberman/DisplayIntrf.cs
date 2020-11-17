using System.Windows.Forms;

interface DisplayIntrf
{
    void init();
    void draw(GameObjectIntr obj);
    void remove(GameObjectIntr obj);
}