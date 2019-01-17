using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class GameManager
    {
        private Player player1;
        private Player player2;
        private bool isPlayer1Turn;
        private Square selectedSquare;
        private CheckState checkState;
        private readonly bool enforceTurns;
        private ChessPiece selectedChessPiece;
        private List<Square> selectedSqValidMoves;

        public ChessBoard Board { get; private set; }

        //
        public delegate void OnResetSelectedSquareColor(List<Square> squares);
        public event OnResetSelectedSquareColor ResetSelectedSquareColor;

        public delegate void OnSetSelectedSquareColor(List<Square> squares);
        public event OnSetSelectedSquareColor SetSelectedSquareColor;

        public delegate void OnDisplayMessage(string body, string title);
        public event OnDisplayMessage DisplayMessage;

        public delegate void OnCheckMate();
        public event OnCheckMate CallCheckMate;

        public GameManager(bool p1IsWhite, bool enforceTurns)
        {
            this.isPlayer1Turn = p1IsWhite;
            this.enforceTurns = enforceTurns;

            Board = new ChessBoard();
            checkState = new CheckState();
            selectedSqValidMoves = new List<Square>();

            List<ChessPiece> p1Set = Board.BuildPlayer1Pieces(p1IsWhite);
            List<ChessPiece> p2Set = Board.BuildPlayer2Pieces(!p1IsWhite);

            player1 = new Player(p1Set, Board, checkState, p1IsWhite);
            player2 = new Player(p2Set, Board, checkState, !p1IsWhite);

            player1.Opponent = player2;
            player2.Opponent = player1;
        }

        // provides support required to interact with logical components,
        // will only move a piece to a valid location on the board.
        // the order which Sqaures are clicked determines if the
        // player will move or just observe possible moves
        public void SquareClicked(Square sq)
        {
            if (selectedSqValidMoves.Contains(sq))
            {
                if (!enforceTurns) MovePiece(sq);
                else ValidatePlayerTurns(sq);
                sq = null;
            }

            if (selectedSqValidMoves.Count > 0)
            {
                ResetSelectedSquareColor(selectedSqValidMoves);
                selectedSqValidMoves.Clear();
            }

            if (sq != null && sq.Piece != null)
            {
                selectedSquare = sq;
                selectedChessPiece = sq.Piece;

                selectedSqValidMoves = player1.IsWhite == selectedChessPiece.IsWhite ?
                                       player1.ValidateMoves(selectedChessPiece) :
                                       player2.ValidateMoves(selectedChessPiece);

                SetSelectedSquareColor(selectedSqValidMoves);
            }

            if (checkState.IsCheckMate)
            {
                //envoke delegate and announce checkmate
                DisplayMessage(checkState.CheckMateMessage, "Check Mate!");
                CallCheckMate();
            }
            else if (checkState.IsCheck)
            {
                DisplayMessage(checkState.CheckMessage, "Check!");
                checkState.IsCheck = false;
                checkState.CheckedKing = null;
            }
        }

        private void MovePiece(Square sq)
        {
            if (player1.IsWhite == selectedChessPiece.IsWhite)
            {
                if (player1.MovePiece(selectedChessPiece, sq))
                {
                    ResetSelectedSquareColor(selectedSqValidMoves);
                }
            }
            else
            {
                if (player2.MovePiece(selectedChessPiece, sq))
                {
                    ResetSelectedSquareColor(selectedSqValidMoves);
                }
            }
        }

        private void ValidatePlayerTurns(Square sq)
        {
            if (selectedSquare.Piece.IsWhite == player1.IsWhite && isPlayer1Turn)
            {
                MovePiece(sq);
                isPlayer1Turn = false;
            }
            else if (selectedSquare.Piece.IsWhite == player2.IsWhite && !isPlayer1Turn)
            {
                MovePiece(sq);
                isPlayer1Turn = true;
            }
            else
            {
                string turn = isPlayer1Turn ? $"{player1.Color}s" : $"{player2.Color}s";
                DisplayMessage($"{turn} turn, this move is not allowed under the current setting", "Enforced Turns Enabled");
            }
            selectedChessPiece = null;
        }
    }
}
