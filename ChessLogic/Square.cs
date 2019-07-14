using System;

namespace ChessLogic
{
    /// <summary>
    /// Represents a single Square on the chess board. 
    /// </summary>
    public sealed class Square
    {
        public delegate void OnPieceChange();

        /// <summary>
        /// The event raised when the piece property changes. This can be used to notify the Gui.
        /// </summary>
        public event OnPieceChange PieceChange;

        public delegate void OnSurfaceColorChange(bool isHighlighted);

        /// <summary>
        /// The event raised when the squares color needs to either be highlighted or regular. This can be used to notify the Gui.
        /// </summary>
        public event OnSurfaceColorChange PaintSquareSurface;

        /// <summary>
        /// The vertical 'y' zero based index of the square, relative to the matrix it is located in.
        /// </summary>
        public byte Row { get; }

        /// <summary>
        /// The horizontal 'x' zero based index of the square, relative to the matrix it is located in.
        /// </summary>
        public byte Column { get; }

        /// <summary>
        /// Indicates wether or not the square currently has a piece positioned on it.
        /// </summary>
        public bool IsOccupied => piece != null;

        /// <summary>
        /// Returns a string representing the squares color.
        /// </summary>
        public string Color => IsWhite ? "white" : "black";

        /// <summary>
        /// Returns a bool representing the squares color.
        /// </summary>
        public bool IsWhite { get; }

        private ChessPiece piece;

        /// <summary>
        /// A reference to a chess piece if there is one currently positioned on the square or a null value if not.
        /// </summary>
        public ChessPiece Piece
        {

            get
            {
                return piece;
            } 
            internal set
            {
                piece = value;
                //PieceChange?.Invoke();
            }
        }

        /// <summary>
        /// Initialize a new instance of Square with the specified properties.
        /// </summary>
        /// <param name="isWhite">Specifies the color of the square, white or black.</param>
        /// <param name="row">Specifies the vertical 'y' zero based index of the square.</param>
        /// <param name="column">Specifies the horizontal 'x' zero based index of the square.</param>
        public Square(bool isWhite, byte row, byte column)
        {
            IsWhite = isWhite;
            Row = row;
            Column = column;
            Piece = null;
        }

        public void PieceChanged()
        {
            PieceChange?.Invoke();
        }

        /// <summary>
        /// Invokes the delegate reponsible for holding a reference to the method which paints the overlaying'Gui Square'
        /// </summary>
        /// <param name="isHighlighted">Indicates if the square surface should be highlighted or displayed as regular</param>
        internal void PaintSelf(bool isHighlighted) => PaintSquareSurface?.Invoke(isHighlighted);

        /// <summary>
        /// Formats a string which represents the squares properties.
        /// </summary>
        /// <returns>Returns a formatted string representing the squares properties.</returns>
        public override string ToString() => IsOccupied ?
        $"{Color} square[{Row}, {Column}] contains piece: {Piece.Color} {Piece.GetType().Name}" :
        $"{Color} square[{Row}, {Column}] contains piece: false";       
    }
}
