using System.Collections.Generic;

namespace ChessLogic
{
    //describe the basic properties and behaviors of a chess piece,
    //contains an implementation for how most chess pieces can be moved,
    //pawn movements are more specific, see - the derived class 'Pawn'

    /// <summary>
    /// The base class for all chess pieces, it represents the basic properties and behaviors of a chess piece.
    /// </summary>
    public abstract class ChessPiece
    {
        /// <summary>
        /// Returns a bool representing the pieces color.
        /// </summary>
        public bool IsWhite { get; }

        /// <summary>
        /// Returns a string representing the pieces color.
        /// </summary>
        public string Color => IsWhite ? "White" : "Black";

        /// <summary>
        /// Gets or sets the pieces current square, the square on which the piece is currently positioned.
        /// </summary>
        public Square CurrentSquare { get; internal set; }

        /// <summary>
        /// Indicates wether or not the piece has moved yet.
        /// </summary>
        public bool HasMoved { get; internal set; }

        public int Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of a ChessPiece with the specified properties.
        /// </summary>
        /// <param name="isWhite">Specifies the color of the piece, white or black.</param>
        /// <param name="square">Specifies the square in which the piece will be positioned.</param>
        public ChessPiece(bool isWhite, Square square)
        {
            IsWhite = isWhite;
            CurrentSquare = square;
            CurrentSquare.Piece = this;
        }

        /// <summary>
        /// Implemented in derived classes, searches the board for all possible moves the current piece can move to.
        /// </summary>
        /// <param name="board">The board in which to search for possible moves.</param>
        /// <returns>Returns a List of Squares that could be considered for the players next move. This is not a validated list of moves.</returns>
        public abstract List<Square> SearchPossibleMoves(ChessBoard board);

        /// <summary>
        /// Searches a straight path until an out of bounds index or another piece is encountered. This movement is useful for Queens, Bishops, Castles.
        /// </summary>
        /// <param name="squares">The Squares matrix to be searched.</param>
        /// <param name="row">The vertical 'y' direction to traverse.</param>
        /// <param name="col">The horizontal 'x' direction to traverse.</param>
        /// <returns>Returns an IEnumerable Square that could be considered for the players next move. This is not a validated list of moves.</returns>
        protected IEnumerable<Square> LinearTraversal(Square[,] squares, int row, int col)
        {
            Square sq;
            int nextRow = CurrentSquare.Row + row;
            int nextCol = CurrentSquare.Column + col;

            while ((nextRow <= 7 && nextRow >= 0) && (nextCol <= 7 && nextCol >= 0))
            {
                sq = squares[nextRow, nextCol];

                if (!sq.IsOccupied) { yield return sq; } //empty square
                else if (IsWhite != sq.Piece.IsWhite) { yield return sq; break; }
                else
                    break;//square is occupied by own color, end line of search

                nextRow += row;
                nextCol += col;
            }
            yield break;
        }

        /// <summary>
        /// Checks if a piece can be placed directly onto the square at the supplied indexes, this movement is useful for Kings and Knights.
        /// </summary>
        /// <param name="squares">The Squares matrix to be searched.</param>
        /// <param name="row">the row 'y' index to check.</param>
        /// <param name="col">the col 'x' index to check.</param>
        /// <returns>Returns the Square at the marked position if it is valid, returns null if it is not.</returns>
        protected Square DirectPlacement(Square[,] squares, int row, int col)
        {
            //checks indexes are in bounds of a typical board
            if (row <= 7 && row >= 0 && col <= 7 && col >= 0)
            {
                if (!squares[row, col].IsOccupied ||
                    squares[row, col].Piece.IsWhite != IsWhite)
                {
                    return (squares[row, col]);
                }
            }
            //square out of array bounds
            //or own piece occupied
            return null;
        }

        /// <summary>
        /// Formats a string which represents the pieces properties.
        /// </summary>
        /// <returns>Returns a formatted string representing the pieces properties.</returns>
        public override string ToString() => $"{GetType().Name} - {Color} -" +
            $" square[{CurrentSquare.Row}, {CurrentSquare.Column}]";
    }
}
