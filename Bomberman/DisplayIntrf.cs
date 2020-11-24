using System.Collections.Generic;
using System.Windows.Forms;

interface DisplayIntrf
{
    void update();
    void draw(List<GameObjectIntr> gameObjects);
}