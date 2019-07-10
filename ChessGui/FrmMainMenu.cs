using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGui
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
            CboColor.Text = CboColor.Items[0].ToString();
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            bool selectedColor =
                 CboColor.SelectedItem != null ?
                 CboColor.Text == "White" ? true : false : true;
            bool enforceTurns = ChkEnforce.Checked;
            FrmGame game = new FrmGame(selectedColor, enforceTurns, this);
            Hide();
            game.ShowDialog();
        }

        private void MenuItemNewGame_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            bool selectedColor = menuItem.Text == "as white" ? true : false;
            bool enforceTurns = ChkEnforce.Checked;
            FrmGame game = new FrmGame(selectedColor, enforceTurns, this);
            Hide();
            game.ShowDialog();
        }

        private void DisplayAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show
                ("Chess Game provides a simple interface for your favourite strategy game.\n" +
                 "Simply click away and place your opponents king under checkmate!\n\n" +
                 "Enjoy the open board approach in this test release.\n\n" +
                 "Developed - Barry Pietersen 2018",
                 "About", MessageBoxButtons.OK);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
