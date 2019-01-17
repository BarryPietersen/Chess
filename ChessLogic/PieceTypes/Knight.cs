using System.Collections.Generic;
using System.Linq;

namespace ChessLogic.PieceTypes
{
    /// <summary>
    /// Represents a knight and its capabilities in moving and attacking an opponent
    /// </summary>
    public sealed class Knight : ChessPiece
    {
        /// <summary>
        /// Initializes a new instance of a Knight with the specified properties.
        /// </summary>
        /// <param name="isWhite">Specifies the color of the piece, white or black.</param>
        /// <param name="square">Specifies the square in which the piece will be positioned.</param>
        public Knight(bool isWhite, Square square)
            : base(isWhite, square) { }

        /// <summary>
        /// Checks 8 possible directions a knight can travel, ignors any obstacle pieces encountered during the line of travel.
        /// </summary>
        /// <param name="board">The board in which to search for possible moves.</param>
        /// <returns>Returns a List of Squares that could be considered for the knights next move. This is not a validated list of moves.</returns>
        public override List<Square> SearchPossibleMoves(ChessBoard board)
        {
            List<Square> moves = new List<Square>(8);
            Square[,] squares = board.Squares;
            int row = CurrentSquare.Row;
            int col = CurrentSquare.Column;

            moves.Add(DirectPlacement(squares, row + 2, col + 1));
            moves.Add(DirectPlacement(squares, row + 2, col - 1));
            moves.Add(DirectPlacement(squares, row - 2, col + 1));
            moves.Add(DirectPlacement(squares, row - 2, col - 1));
            moves.Add(DirectPlacement(squares, row + 1, col + 2));
            moves.Add(DirectPlacement(squares, row + 1, col - 2));
            moves.Add(DirectPlacement(squares, row - 1, col + 2));
            moves.Add(DirectPlacement(squares, row - 1, col - 2));

            IEnumerable<Square> validMoves = from sq in moves
                                             where sq != null
                                             select sq;
            
            return validMoves.ToList();
        }
    }
}
