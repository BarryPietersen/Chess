using ChessLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGui
{
    public partial class FrmGame : Form
    {
        private GameManager game;
        private FrmMainMenu menu;
        private readonly bool isP1White;
        private readonly bool enforceTurns;

        public FrmGame(bool isP1White, bool enforceTurns, FrmMainMenu menu)
        {
            this.menu = menu;
            this.isP1White = isP1White;
            this.enforceTurns = enforceTurns;

            game = new GameManager(isP1White, enforceTurns);
            game.DisplayMessage += DisplayMessage;
            game.CallCheckMate += CheckMate;

            InitializeComponent();
            InitializeGuiSquares(game.Board.Squares);
        }

        private void GuiSquare_Click(object sender, EventArgs e)
        {
            GuiSquare gsq = (GuiSquare)sender;
            game.SquareClicked(gsq.Square);
        }

        private void DisplayMessage(string body, string title) => MessageBox.Show(body, title);

        private void CheckMate()
        {
            DisconnectGuiSquareHandler();
        }

        private void DisconnectGuiSquareHandler()
        {
            foreach (Control ctl in Controls)
            {
                if (ctl is GuiSquare gsq)
                {
                    gsq.Click -= GuiSquare_Click;
                }
            }
        }

        //can be used to generate guisquares dynamically.
        //not used in the program
        //private List<GuiSquare> guiSquares = new List<GuiSquare>(64);
        private void GenerateGuiSquares(Square[,] squares)
        {
            int x;
            int y = 35;

            for (int i = 0; i < 8; i++)
            {
                x = 38;
                for (int j = 0; j < 8; j++)
                {
                    GuiSquare gsq = new GuiSquare(new Point(x, y), squares[i, j]);                
                    gsq.Click += new EventHandler(GuiSquare_Click);
                    Controls.Add(gsq);
                    x += 85;
                }
                y += 85;
            }
        }

        private void InitializeGuiSquares(Square[,] logicSquares)
        {
            guiSquare1.Square = logicSquares[0, 0];
            guiSquare2.Square = logicSquares[0, 1];
            guiSquare3.Square = logicSquares[0, 2];
            guiSquare4.Square = logicSquares[0, 3];
            guiSquare5.Square = logicSquares[0, 4];
            guiSquare6.Square = logicSquares[0, 5];
            guiSquare7.Square = logicSquares[0, 6];
            guiSquare8.Square = logicSquares[0, 7];
            guiSquare9.Square = logicSquares[1, 0];
            guiSquare10.Square = logicSquares[1, 1];
            guiSquare11.Square = logicSquares[1, 2];
            guiSquare12.Square = logicSquares[1, 3];
            guiSquare13.Square = logicSquares[1, 4];
            guiSquare14.Square = logicSquares[1, 5];
            guiSquare15.Square = logicSquares[1, 6];
            guiSquare16.Square = logicSquares[1, 7];
            guiSquare17.Square = logicSquares[2, 0];
            guiSquare18.Square = logicSquares[2, 1];
            guiSquare19.Square = logicSquares[2, 2];
            guiSquare20.Square = logicSquares[2, 3];
            guiSquare21.Square = logicSquares[2, 4];
            guiSquare22.Square = logicSquares[2, 5];
            guiSquare23.Square = logicSquares[2, 6];
            guiSquare24.Square = logicSquares[2, 7];
            guiSquare25.Square = logicSquares[3, 0];
            guiSquare26.Square = logicSquares[3, 1];
            guiSquare27.Square = logicSquares[3, 2];
            guiSquare28.Square = logicSquares[3, 3];
            guiSquare29.Square = logicSquares[3, 4];
            guiSquare30.Square = logicSquares[3, 5];
            guiSquare31.Square = logicSquares[3, 6];
            guiSquare32.Square = logicSquares[3, 7];
            guiSquare33.Square = logicSquares[4, 0];
            guiSquare34.Square = logicSquares[4, 1];
            guiSquare35.Square = logicSquares[4, 2];
            guiSquare36.Square = logicSquares[4, 3];
            guiSquare37.Square = logicSquares[4, 4];
            guiSquare38.Square = logicSquares[4, 5];
            guiSquare39.Square = logicSquares[4, 6];
            guiSquare40.Square = logicSquares[4, 7];
            guiSquare41.Square = logicSquares[5, 0];
            guiSquare42.Square = logicSquares[5, 1];
            guiSquare43.Square = logicSquares[5, 2];
            guiSquare44.Square = logicSquares[5, 3];
            guiSquare45.Square = logicSquares[5, 4];
            guiSquare46.Square = logicSquares[5, 5];
            guiSquare47.Square = logicSquares[5, 6];
            guiSquare48.Square = logicSquares[5, 7];
            guiSquare49.Square = logicSquares[6, 0];
            guiSquare50.Square = logicSquares[6, 1];
            guiSquare51.Square = logicSquares[6, 2];
            guiSquare52.Square = logicSquares[6, 3];
            guiSquare53.Square = logicSquares[6, 4];
            guiSquare54.Square = logicSquares[6, 5];
            guiSquare55.Square = logicSquares[6, 6];
            guiSquare56.Square = logicSquares[6, 7];
            guiSquare57.Square = logicSquares[7, 0];
            guiSquare58.Square = logicSquares[7, 1];
            guiSquare59.Square = logicSquares[7, 2];
            guiSquare60.Square = logicSquares[7, 3];
            guiSquare61.Square = logicSquares[7, 4];
            guiSquare62.Square = logicSquares[7, 5];
            guiSquare63.Square = logicSquares[7, 6];
            guiSquare64.Square = logicSquares[7, 7];
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Close();
            Hide();
            Dispose();
            new FrmGame(isP1White, enforceTurns, menu).ShowDialog();
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            Close();
            Hide();
            menu.Show();
        }

        private void FrmGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }
    }
}
