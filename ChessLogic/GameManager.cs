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
        private ChessPiece selectedPiece;
        private List<Square> selectedPieceValidMoves;

        public ChessBoard Board { get; }

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
            selectedPieceValidMoves = new List<Square>();

            List<ChessPiece> p1Set = PieceFactory.BuildPlayer1Pieces(p1IsWhite, Board.Squares);
            List<ChessPiece> p2Set = PieceFactory.BuildPlayer2Pieces(!p1IsWhite, Board.Squares);

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
            if (selectedPieceValidMoves.Contains(sq))
            {
                if (!enforceTurns) MovePiece(sq);
                else ValidatePlayerTurns(sq);
                sq = null;
            }

            if (selectedPieceValidMoves.Count > 0)
            {
                PaintValidSurfaces(false);
                selectedPieceValidMoves.Clear();
            }

            if (sq != null && sq.IsOccupied)
            {
                selectedSquare = sq;
                selectedPiece = sq.Piece;

                selectedPieceValidMoves = player1.IsWhite == selectedPiece.IsWhite ?
                                          player1.ValidateMoves(selectedPiece) :
                                          player2.ValidateMoves(selectedPiece);

                PaintValidSurfaces(true);
            }

            if (checkState.IsCheckMate)
            {
                // envoke delegate and announce checkmate
                DisplayMessage?.Invoke(checkState.CheckMateMessage, "Check Mate!");
                CallCheckMate?.Invoke();
            }
            else if (checkState.IsCheck)
            {
                DisplayMessage?.Invoke(checkState.CheckMessage, "Check!");
                checkState.IsCheck = false;
                checkState.CheckedKing = null;
            }
        }

        private void MovePiece(Square sq)
        {
            if (player1.IsWhite == selectedPiece.IsWhite)
            {
                if (player1.MovePiece(selectedPiece, sq))
                {
                    PaintValidSurfaces(false);
                }
            }
            else
            {
                if (player2.MovePiece(selectedPiece, sq))
                {
                    PaintValidSurfaces(false);
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
                DisplayMessage?.Invoke($"{turn} turn, this move is not allowed under the current setting", "Enforced Turns Enabled");
            }
            selectedPiece = null;
        }

        private void PaintValidSurfaces(bool isHighlighted)
        {
            foreach (Square sq in selectedPieceValidMoves)
            {
                sq.PaintSelf(isHighlighted);
            }
        }
    }
}
