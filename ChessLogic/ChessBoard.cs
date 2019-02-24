using ChessLogic.PieceTypes;
using System;
using System.Collections.Generic;

namespace ChessLogic
{
    /// <summary>
    /// Represents a typical chess board of an 8 x 8 matrix
    /// </summary>
    public class ChessBoard
    {
        public Square[,] Squares { get; }

        /// <summary>
        /// Initialize a new instance of a Board with the traditional size, an 8 x 8 matrix of squares.
        /// </summary>
        public ChessBoard()
        {
            Squares = BuildSquares();
        }

        // a traditional pattern of alternating colors
        // starting with the top left square as white
        private Square[,] BuildSquares()
        {
            bool iswhite = true;
            Square[,] squares = new Square[8, 8];

            for (byte i = 0; i < 8; i++)
            {
                squares[i, 0] = new Square(iswhite, i, 0);
                squares[i, 1] = new Square(!iswhite, i, 1);
                squares[i, 2] = new Square(iswhite, i, 2);
                squares[i, 3] = new Square(!iswhite, i, 3);
                squares[i, 4] = new Square(iswhite, i, 4);
                squares[i, 5] = new Square(!iswhite, i, 5);
                squares[i, 6] = new Square(iswhite, i, 6);
                squares[i, 7] = new Square(!iswhite, i, 7);

                iswhite = !iswhite;
            }

            return squares;
        }
    }
}
