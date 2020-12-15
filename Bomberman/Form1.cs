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
            hideAll();
            game = new Game(mainPictureBox, true);
            game.GameOver += GameOver;
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

        public void GameOver()
        {
            showAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hideAll();
            game = new Game(mainPictureBox, false);
            game.GameOver += GameOver;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hideAll()
        {
            startButton.Enabled = false;
            startButton.Visible = false;
            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
        }

        private void showAll()
        {
            startButton.Enabled = true;
            startButton.Visible = true;
            button1.Enabled = true;
            button1.Visible = true;
            button2.Enabled = true;
            button2.Visible = true;
        }
    }
}
