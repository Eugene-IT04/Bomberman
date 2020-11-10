using System.Windows.Forms;

interface DisplayIntrf
{
    void init(PictureBox pictureBox);
    void draw(GameObjectIntr obj);
    void remove(GameObjectIntr obj);
}