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
        public Square[,] Squares;

        /// <summary>
        /// Initialize a new instance of a Board with the traditional size, an 8 x 8 matrix of squares.
        /// </summary>
        public ChessBoard()
        {
            Squares = BuildBoardSquares();
        }

        //populates a square[,] with new square objects as a chess board would appear
        private Square[,] BuildBoardSquares()
        {
            Square[,] squares = new Square[8, 8];

            byte r = 0;//row index
            byte w = 0;//column index
            byte b = 1;//column index

            while (r <= 7)
            {
                if (r % 2 == 0)
                {
                    squares[r, w] = new Square(true, r, w);
                    squares[r, b] = new Square(false, r, b);
                }
                else
                {
                    squares[r, w] = new Square(false, r, w);
                    squares[r, b] = new Square(true, r, b);
                }
               
                w += 2;
                b += 2;

                if (w == 8)
                {
                    w = 0;
                    b = 1;
                    r++;
                }
            }
            return squares;
        }

        //all code assumes player1 pieces to be positioned
        //at the front(bottom) of the boards Square[,] on the 
        //row indexes 6 and 7, as it would appear on a chess board
        public List<ChessPiece> BuildPlayer1Pieces(bool isWhite)
        {
            List<ChessPiece> p1 = new List<ChessPiece>(16);
            for (byte i = 0; i < 8; i++)
            {             
                Squares[6, i].Piece = new Pawn(isWhite, Squares[6, i]);
                p1.Add(Squares[6, i].Piece);
            }

            BuildPieces(p1, isWhite, 7);

            return p1;
        }

        public List<ChessPiece> BuildPlayer2Pieces(bool isWhite)
        {
            List<ChessPiece> p2 = new List<ChessPiece>(16);

            for (byte i = 0; i < 8; i++)
            {
                Squares[1, i].Piece = new Pawn(isWhite, Squares[1, i]);
                p2.Add(Squares[1, i].Piece);
            }

            BuildPieces(p2, isWhite, 0);

            return p2;
        }

        //builds and positions rook, knight, bishop, queen and king pieces.
        //cases 3 and 4 perform a check to ensure correct placement of queens
        private void BuildPieces(List<ChessPiece> player, bool isWhite, byte row)
        {
            for (byte i = 0; i <= 7; i++)
            {
                switch (i)
                {
                    case 0:
                    case 7:
                        Squares[row, i].Piece = new Rook(isWhite, Squares[row, i]);
                        player.Add(Squares[row, i].Piece);
                        break;
                    case 1:
                    case 6:
                        Squares[row, i].Piece = new Knight(isWhite, Squares[row, i]);
                        player.Add(Squares[row, i].Piece);
                        break;
                    case 2:
                    case 5:
                        Squares[row, i].Piece = new Bishop(isWhite, Squares[row, i]);
                        player.Add(Squares[row, i].Piece);
                        break;
                    case 3:
                        if ((row == 7 && isWhite == true) || (row == 0 && isWhite == false))
                        {
                            Squares[row, i].Piece = new Queen(isWhite, Squares[row, i]);
                            player.Add(Squares[row, i].Piece);
                        }
                        else
                        {
                            Squares[row, i].Piece = new King(isWhite, Squares[row, i]);
                            player.Add(Squares[row, i].Piece);
                        }                       
                        break;
                    case 4:
                        if ((row == 7 && isWhite == false) || (row == 0 && isWhite == true))
                        {
                            Squares[row, i].Piece = new Queen(isWhite, Squares[row, i]);
                            player.Add(Squares[row, i].Piece);
                        }
                        else
                        {
                            Squares[row, i].Piece = new King(isWhite, Squares[row, i]);
                            player.Add(Squares[row, i].Piece);
                        }
                        break;
                    default:
                        throw new Exception("build player set, switch statement 'row' argument was out of bounds of the array");
                }
            }         
        }
    }
}
