using System.Collections.Generic;
using System.Linq;

namespace ChessLogic.PieceTypes
{
    /// <summary>
    /// Represents a king and its capabilities in moving and attacking an opponent
    /// </summary>
    public sealed class King : ChessPiece
    {
        /// <summary>
        /// Initializes a new instance of a King with the specified properties.
        /// </summary>
        /// <param name="isWhite">Specifies the color of the piece, white or black.</param>
        /// <param name="square">Specifies the square in which the piece will be positioned.</param>
        public King(bool isWhite, Square square)
            : base(isWhite, square) { }

        /// <summary>
        /// Checks 8 possible directions a king can travel.
        /// </summary>
        /// <param name="board">The board in which to search for possible moves.</param>
        /// <returns>Returns a List of Squares that could be considered for the kings next move. This is not a validated list of moves.</returns>
        public override List<Square> SearchPossibleMoves(ChessBoard board)
        {
            List<Square> moves = new List<Square>(8);
            Square[,] squares = board.Squares;
            int row = CurrentSquare.Row;
            int col = CurrentSquare.Column;

            moves.Add(DirectPlacement(squares, row - 1, col - 1));
            moves.Add(DirectPlacement(squares, row - 1, col + 1));
            moves.Add(DirectPlacement(squares, row + 1, col - 1));
            moves.Add(DirectPlacement(squares, row + 1, col + 1));
            moves.Add(DirectPlacement(squares, row - 1, col));
            moves.Add(DirectPlacement(squares, row + 1, col));
            moves.Add(DirectPlacement(squares, row, col - 1));
            moves.Add(DirectPlacement(squares, row, col + 1));

            if (!HasMoved) moves.AddRange(ConsiderCastling(squares));

            IEnumerable<Square> validMoves = from sq in moves
                                             where sq != null
                                             select sq;

            return validMoves.ToList();
        }

        //applies the castling rules to the kings moves
        private IEnumerable<Square> ConsiderCastling(Square[,] squares)
        {
            List<Square> castlingMoves = new List<Square>();
            bool isPathClear = true;

            if (squares[CurrentSquare.Row, 0].IsOccupied &&
                squares[CurrentSquare.Row, 0].Piece is Rook rookA &&
                !rookA.HasMoved)
            {
                for (int i = 1; i < CurrentSquare.Column; i++)
                    if (squares[CurrentSquare.Row, i].IsOccupied) { isPathClear = false; break; }

                if (isPathClear) { yield return squares[CurrentSquare.Row, CurrentSquare.Column - 2]; }
            }

            isPathClear = true;

            if (squares[CurrentSquare.Row, 7].IsOccupied &&
                squares[CurrentSquare.Row, 7].Piece is Rook rookB &&
                !rookB.HasMoved)
            {
                for (int i = 6; i > CurrentSquare.Column; i--)
                    if (squares[CurrentSquare.Row, i].IsOccupied) { isPathClear = false; break; }

                if (isPathClear) { yield return squares[CurrentSquare.Row, CurrentSquare.Column + 2]; }
            }

            yield break;
        }
    }
}
