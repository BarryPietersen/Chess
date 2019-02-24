using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLogic.PieceTypes;

namespace ChessLogic
{
    internal static class PieceFactory
    {
        // all code assumes player1 pieces to be positioned
        // at the front(bottom) of the boards Square[,] on the 
        // row indexes 6 and 7, as it would appear on a chess board
        public static List<ChessPiece> BuildPlayer1Pieces(bool isWhite, Square[,] squares)
        {
            List<ChessPiece> p1set = new List<ChessPiece>(16);

            BuildPawns(p1set, isWhite, 6, squares);
            BuildMajors(p1set, isWhite, 7, squares);

            return p1set;
        }

        public static List<ChessPiece> BuildPlayer2Pieces(bool isWhite, Square[,] squares)
        {
            List<ChessPiece> p2set = new List<ChessPiece>(16);

            BuildPawns(p2set, isWhite, 1, squares);
            BuildMajors(p2set, isWhite, 0, squares);

            return p2set;
        }

        // builds and positions pawns
        private static void BuildPawns(List<ChessPiece> pieces, bool isWhite, byte row, Square[,] squares)
        {
            for (byte i = 0; i < 8; i++)
            {
                squares[1, i].Piece = new Pawn(isWhite, squares[row, i]);
                pieces.Add(squares[1, i].Piece);
            }
        }

        // builds and positions rook, knight, bishop, king and queen pieces.
        private static void BuildMajors(List<ChessPiece> player, bool isWhite, byte row, Square[,] squares)
        {
            // rooks
            squares[row, 0].Piece = new Rook(isWhite, squares[row, 0]);
            squares[row, 7].Piece = new Rook(isWhite, squares[row, 7]);

            // knights
            squares[row, 1].Piece = new Knight(isWhite, squares[row, 1]);
            squares[row, 6].Piece = new Knight(isWhite, squares[row, 6]);

            // bishops
            squares[row, 2].Piece = new Bishop(isWhite, squares[row, 2]);
            squares[row, 5].Piece = new Bishop(isWhite, squares[row, 5]);

            // kings and queens
            // this condition looks to place the queen on 
            // the square who's color matches its itself 
            // 'square.color == queen.color'
            if ((row == 7 && isWhite == true) || (row == 0 && isWhite == false))
            {
                squares[row, 4].Piece = new King(isWhite, squares[row, 4]);
                squares[row, 3].Piece = new Queen(isWhite, squares[row, 3]);
            }
            else
            {
                squares[row, 3].Piece = new King(isWhite, squares[row, 3]);
                squares[row, 4].Piece = new Queen(isWhite, squares[row, 4]);
            }

            // add the newly created pieces to the players pieces list
            for (byte i = 0; i <= 7; i++)
            {
                player.Add(squares[row, i].Piece);
            }
        }
    }
}
