using System.Collections.Generic;

namespace ChessLogic.PieceTypes
{
    /// <summary>
    /// Represents a queen and its capabilities in moving and attacking an opponent
    /// </summary>
    public sealed class Queen : ChessPiece
    {
        /// <summary>
        /// Initializes a new instance of a Queen with the specified properties.
        /// </summary>
        /// <param name="isWhite">Specifies the color of the piece, white or black.</param>
        /// <param name="square">Specifies the square in which the piece will be positioned.</param>
        public Queen(bool isWhite, Square square)
            : base(isWhite, square)
        {
            Value = 1000;
        }

        /// <summary>
        /// Traverse outward in a queens four straight and four diagonal paths until an out of bounds index or another piece is encountered.
        /// </summary>
        /// <param name="board">The board in which to search for possible moves.</param>
        /// <returns>Returns a List of Squares that could be considered for the queens next move. This is not a validated list of moves.</returns>
        public override List<Square> SearchPossibleMoves(ChessBoard board)
        {
            Square[,] squares = board.Squares;
            List<Square> moves = new List<Square>();

            moves.AddRange(LinearTraversal(squares, 1, 1));
            moves.AddRange(LinearTraversal(squares, 1, -1));
            moves.AddRange(LinearTraversal(squares, -1, -1));
            moves.AddRange(LinearTraversal(squares, -1, 1));
            moves.AddRange(LinearTraversal(squares, -1, 0));
            moves.AddRange(LinearTraversal(squares, 0, 1));
            moves.AddRange(LinearTraversal(squares, 1, 0));
            moves.AddRange(LinearTraversal(squares, 0, -1));
            moves.TrimExcess();

            return moves;
        }
    }
}
