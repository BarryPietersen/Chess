using ChessLogic.PieceTypes;
using System.Collections.Generic;

namespace ChessLogic
{
    /*
        En passant is a special pawn capture that can only occur immediately after a pawn makes a double-step move
        from its starting square, and it could have been captured by an enemy pawn had it advanced only one square.
        The opponent captures the just-moved pawn "as it passes" through the first square. The result is
        the same as if the pawn had advanced only one square and the enemy pawn had captured it normally.
        The en passant capture must be made on the very next turn or the right to do so is lost.
        Like any other move, if an en passant capture is the only legal move available, it must be played
    */
    
    /// <summary>
    /// Provides support to identify and manage current en passant opportunities for all pawns on the board, maintains a look up table 
    /// </summary>
    public static class EnPassantTracker
    {
        public static bool HasValue { get; set; }

        /// <summary>
        /// Stores the up to date key value pairs of current en passant details. There could only ever exist at most two current enpassant cases, as a result from the same pawn move.
        /// </summary>
        public static Dictionary<ChessPiece, Square> CurrentEnPassants = new Dictionary<ChessPiece, Square>(2);

        /// <summary>
        /// Determines a pawns vulnerability to the enpassant rule. Call this method when the pawn has been moved for the first time and is positioned on its new square.
        /// </summary>
        /// <param name="pawn">The pawn to qualify as an enpassant capture</param>
        /// <param name="board">The current chess board with the populated squares matrix</param>
        public static void AnalyseEnpassantConditions(Pawn pawn, ChessBoard board)
        {
            //check if the pawn has been moved only one row
            if (pawn.CurrentSquare.Row == 5 || pawn.CurrentSquare.Row == 2) { return; }

            sbyte col = (sbyte)(pawn.CurrentSquare.Column);

            for (sbyte i = -1; i < 2; i += 2)
            {
                if (col + i >= 0 && col + i <= 7)
                {
                    if (board.Squares[pawn.CurrentSquare.Row, col + i].IsOccupied &&
                        board.Squares[pawn.CurrentSquare.Row, col + i].Piece.IsWhite != pawn.IsWhite &&
                        board.Squares[pawn.CurrentSquare.Row, col + i].Piece is Pawn enPassantPawn)
                    {
                        CurrentEnPassants.Add(enPassantPawn,
                        board.Squares[(pawn.CurrentSquare.Row == 4 ? 5 : 2), pawn.CurrentSquare.Column]);
                    }
                }
            }
        }
    }
}
