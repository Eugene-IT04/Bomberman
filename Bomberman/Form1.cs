using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    public partial class mainForm : Form
    {
        Game game;
        public mainForm()
        {
            InitializeComponent();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = false;
            startButton.Enabled = false;
            game = new Game(mainPictureBox);
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            game.keyDown(e.KeyCode);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            game.keyUp(e.KeyCode);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}
