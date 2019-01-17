using ChessLogic;
using ChessLogic.PieceTypes;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace ChessGui
{
    class GuiSquare : UserControl
    {
        private Square square;
        public Square Square
        {
            get { return square; }
            set
            {
                square = value;
                if (value != null)
                {
                    square.PieceChange += OnPieceChange;
                    square.PaintSquareSurface += PaintSquareSurface;
                    UpdateGuiSquare();
                }
            }
        }

        private void PaintSquareSurface(bool isHighlighted)
        {
            if (isHighlighted) BackColor = Color.Tan;
            else
                BackColor = Square.IsWhite ? SystemColors.ButtonFace : SystemColors.WindowFrame;
        }

        // not used -
        // this constructor can be used to generate new GuiSquare objects 
        // through the FrmGame designer code by using loops and incrementing 
        // Point variable sums can be calculated in the loop
        public GuiSquare(Point location, Square sq)
        {
            Square = sq;
            Location = location;   
            Size = new Size(80, 80);
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Square.IsWhite ? SystemColors.ButtonFace : SystemColors.WindowFrame;
        }

        public GuiSquare() { }

        // this event handler responds to the logical squares PieceChange delegate object
        // the method pointer is assigned this handler in the 'setter' of the Square property
        public void OnPieceChange() => UpdateGuiSquare();

        public void UpdateGuiSquare()
        {
            if (Square.Piece != null)
                DrawGuiSquareImage(Square);
            else
                BackgroundImage = null;
        }

        private void DrawGuiSquareImage(Square sq)
        {
            ChessPiece piece = sq.Piece;

            if (piece is Pawn)
            {
                BackgroundImage = piece.IsWhite == true ?
                    Properties.Resources.whitePawn :
                    Properties.Resources.blackPawn;
            }
            else if (piece is Rook)
            {
                BackgroundImage = piece.IsWhite == true ?
                    Properties.Resources.whiteRook :
                    Properties.Resources.blackRook;
            }
            else if (piece is Bishop)
            {
                BackgroundImage = piece.IsWhite == true ?
                    Properties.Resources.whiteBishop :
                    Properties.Resources.blackBishop;
            }
            else if (piece is Knight)
            {
                BackgroundImage = piece.IsWhite == true ?
                    Properties.Resources.whiteKnight :
                    Properties.Resources.blackKnight;
            }
            else if (piece is Queen)
            {
                BackgroundImage = piece.IsWhite == true ?
                     Properties.Resources.whiteQueen :
                     Properties.Resources.blackQueen;
            }
            else if (piece is King)
            {
                BackgroundImage = piece.IsWhite == true ?
                    Properties.Resources.whiteKing :
                    Properties.Resources.blackKing;
            }
            else
                throw new Exception($"Unrecognised Piece Type: {piece.GetType()}");
        }
    }
}
