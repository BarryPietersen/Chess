using System.Collections.Generic;

namespace ChessLogic.PieceTypes
{
    /// <summary>
    /// Represents a pawn and its capabilities in moving and attacking an opponent.
    /// </summary>
    public sealed class Pawn : ChessPiece
    {
        private readonly byte startingRow;

        /// <summary>
        /// Initializes a new instance of a Pawn with the specified properties.
        /// </summary>
        /// <param name="isWhite">Specifies the color of the piece, white or black.</param>
        /// <param name="square">Specifies the square in which the piece will be positioned.</param>
        public Pawn(bool isWhite, Square square)
            : base(isWhite, square) { startingRow = CurrentSquare.Row; }

        /// <summary>
        /// Scans the forward proximity of the pawns current position for possible moves. Communicates with the enpassant tracker.
        /// </summary>
        /// <param name="board">The board in which to search for possible moves.</param>
        /// <returns>Returns a List of Squares that could be considered for the pawns next move. This is not a validated list of moves.</returns>
        public override List<Square> SearchPossibleMoves(ChessBoard board)
        {
            List<Square> moves = new List<Square>();

            //determines which direction the pawn moves
            sbyte nextRow = (sbyte)(startingRow == 6 ? -1 : 1);

            //qualifies the forward two squares for legalMoves
            if (!HasMoved)
            {
                Square square1 = board.Squares[CurrentSquare.Row + nextRow, CurrentSquare.Column];
                Square square2 = board.Squares[CurrentSquare.Row + nextRow * 2, CurrentSquare.Column];
                if (square2.Piece == null && square1.Piece == null) moves.Add(square2);
            }

            for (int i = -1; i < 2; i++)
            {
                //checks index against bounds of the squares[,]
                if ((CurrentSquare.Column + i <= 7 && CurrentSquare.Column + i >= 0) &&
                    (CurrentSquare.Row <= 6 && CurrentSquare.Row >= 1))
                {
                    Square square = board.Squares[CurrentSquare.Row + nextRow, CurrentSquare.Column + i];

                    if (!square.IsOccupied && CurrentSquare.Column == square.Column) { moves.Add(square); }
                    else if (square.IsOccupied && (square.Piece.IsWhite != IsWhite && square.Column != CurrentSquare.Column))
                    {
                        //destroyable piece found
                        moves.Add(square);                  
                    }
                }
            }

            if (EnPassantTracker.CurrentEnPassants.ContainsKey(this))
            {
                moves.Add(EnPassantTracker.CurrentEnPassants[this]);
                EnPassantTracker.HasValue = true;
            }

            return moves;
        }
    }
}
