using ChessLogic.PieceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class PlayerAi : Player
    {
        public PlayerAi(List<ChessPiece> pieceSet, ChessBoard board,
            CheckState checkState, bool isWhite) : base (pieceSet, board, checkState, isWhite){ }

        public void Think()
        {
            MoveState ms = new MoveState(this, Opponent);
            List<Tuple<ChessPiece, Square, int>> moves = new List<Tuple<ChessPiece, Square, int>>();

            foreach (var p in PieceSet.ToList())
            {
                foreach (var sq in ValidateMoves(p))
                {
                    int v = MovePiece(p, sq, ms);
                    v += DfsThink(Opponent, this);
                    moves.Add(new Tuple<ChessPiece, Square, int>(p, sq, v));
                    ms.Restore();
                }
            }

            // determine the highest value move in moves
            // or if none use a different strategy to analyse
            // the best move

            var max = moves.Max(m => m.Item3);
            var highest = moves.Where(m => m.Item3 == max).ToList();

            var move = highest[new Random().Next(0, highest.Count - 1)];

            MovePiece(move.Item1, move.Item2);
        }

        // the recursive method is time consuming and requires debugging
        // for now just searching one move deep and selecting the highest
        // value capture
        private int DfsThink(Player p1, Player p2, int depth = 0)
        {
            if (depth == 0) return 0;
            int value = 0;

            MoveState ms = new MoveState(p1, p2);

            foreach (var p in p1.PieceSet.ToList())
            {
                foreach (var sq in ValidateMoves(p))
                {
                    int val = MovePiece(p, sq, ms);
                    val = depth % 2 == 0 ? -val : val;
                    val += DfsThink(p2, p1, depth - 1);
                    value = Math.Max(val, value);
                    ms.Restore();
                }
            }

            return value;
        }

        private int MovePiece(ChessPiece piece, Square square, MoveState moveState)
        {
            int value = 0;

            if (square.IsOccupied)
            {
                ChessPiece p = square.Piece;
                moveState.P2Pieces.Add(new Tuple<ChessPiece, Square, bool>(p, p.CurrentSquare, p.HasMoved));
                value = square.Piece.Value;

                CapturePiece(p);
            }
            // specific condition - checks to see if an 'enpassant' move has been made
            else if (piece is Pawn && square.Column != piece.CurrentSquare.Column)
            {
                ChessPiece p = Board.Squares[(piece.CurrentSquare.Row == 3 ? 3 : 4), square.Column].Piece;
                moveState.P2Pieces.Add(new Tuple<ChessPiece, Square, bool>(p, p.CurrentSquare, p.HasMoved));
                value = p.Value;

                CapturePiece(p);
            }
            else
            {
                moveState.ToSquare = square;
            }

            moveState.P1Pieces.Add(new Tuple<ChessPiece, Square, bool>(piece, piece.CurrentSquare, piece.HasMoved));
            PositionPiece(piece, square);

            if (!piece.HasMoved)
            {
                if (piece is Pawn pawn) EnPassantTracker.AnalyseEnpassantConditions(pawn, Board);
                else if (piece is King king)
                {
                    AnalyseCastlingConditions(king);
                }

                piece.HasMoved = true;
            }

            // also checks if the pawn
            // qualifies for a promotion
            // in its new position
            if (piece is Pawn && (piece.CurrentSquare.Row == 0 || piece.CurrentSquare.Row == 7))
            {
                piece = PromotePawn(piece);
            }

            if (EnPassantTracker.HasValue)
            {
                EnPassantTracker.CurrentEnPassants.Clear();
                EnPassantTracker.HasValue = false;
            }

            if (IsCheck(piece))
            {
                value = 150;
                if (IsCheckMate())
                {
                    value = 1000;
                }
            }

            return value;
        }

        private class MoveState
        {
            public Square ToSquare { get; set; }
            public Player P1 { get; set; }
            public Player P2 { get; set; }
            public List<Tuple<ChessPiece, Square, bool>> P1Pieces { get; set; }
            public List<Tuple<ChessPiece, Square, bool>> P2Pieces { get; set; }

            public MoveState(Player p1, Player p2)
            {
                P1 = p1;
                P2 = p2;

                P1Pieces = new List<Tuple<ChessPiece, Square, bool>>();
                P2Pieces = new List<Tuple<ChessPiece, Square, bool>>();
            }

            public void Restore()
            {
                foreach (var tup in P1Pieces)
                {
                    tup.Item1.HasMoved = tup.Item3;
                    tup.Item1.CurrentSquare = tup.Item2;
                    tup.Item2.Piece = tup.Item1;
                }

                foreach (var tup in P2Pieces)
                {
                    tup.Item1.HasMoved = tup.Item3;
                    tup.Item1.CurrentSquare = tup.Item2;
                    tup.Item2.Piece = tup.Item1;

                    P2.PieceSet.Add(tup.Item1);
                }

                if (ToSquare != null) ToSquare.Piece = null;
            }
        }
    }
}
