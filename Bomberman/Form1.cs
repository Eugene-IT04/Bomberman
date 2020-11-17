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
        //test
        private void mainPictureBox_Click(object sender, EventArgs e)
        {
            game = new Game(mainPictureBox);
        }
        //test
    }
}
