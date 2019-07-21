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
        public int milliSeconds { get; set; } = 800;
        public bool AnimateThoughts { get; set; }
        public PlayerAi(List<ChessPiece> pieceSet, ChessBoard board,
            CheckState checkState, bool isWhite) : base (pieceSet, board, checkState, isWhite){ }

        public void Think()
        {
            MoveState ms;
            List<Tuple<ChessPiece, Square, int>> moves = new List<Tuple<ChessPiece, Square, int>>();

            foreach (var p in PieceSet.ToList())
            {
                foreach (var sq in ValidateMoves(p))
                {
                    ms = new MoveState(this, Opponent);
                    int v = MovePiece(this, Opponent, p, sq, ms);

                    if (AnimateThoughts)
                    {
                        Task.Delay(milliSeconds).Wait();
                    }

                    v += DfsThink(Opponent, this);
                    moves.Add(new Tuple<ChessPiece, Square, int>(p, sq, v));
                    ms.Restore(AnimateThoughts);
                }
            }

            // determine the highest value move in moves
            // or if none use a different strategy to analyse
            // the best move
            if (moves.Count > 0)
            {
                var max = moves.Max(m => m.Item3);
                var highest = moves.Where(m => m.Item3 == max).ToList();
                var move = highest[new Random().Next(0, highest.Count - 1)];

                MovePiece(move.Item1, move.Item2);
            }
            else if (IsCheckMate())
            {
                CheckState.IsCheckMate = true;
                CheckState.CheckMateKing = (King)PieceSet.Where(p => p is King).First();
            }
            else
            {
                CheckState.IsStaleMate = true;
                CheckState.StaleMateKing = (King)PieceSet.Where(p => p is King).First();
            }
        }

        /*
            Pawn    100
            Knight  350
            Bishop  350
            Rook    500
            Queen   1000
            King    10000
        */

        // the recursive method is time consuming,
        // for now just searching three moves deep
        // and selecting the highest value capture
        private int DfsThink(Player p1, Player p2, int depth = 2)
        {
            if (depth == 0) return 0;

            int currentVal;
            int currentDfs;
            int dfsValue = 0;
            int currentMax = int.MinValue;

            foreach (var p in p1.PieceSet.ToList())
            {
                foreach (var sq in p1.ValidateMoves(p))
                {
                    MoveState ms = new MoveState(p1, p2);
                    currentVal = MovePiece(p1, p2, p, sq, ms);

                    if (AnimateThoughts)
                    {
                        Task.Delay(milliSeconds).Wait();
                    }

                    currentDfs = DfsThink(p2, p1, depth - 1);

                    if (currentVal > currentMax)
                    {
                        currentMax = currentVal;
                        dfsValue = currentDfs;
                    }

                    ms.Restore(AnimateThoughts);
                }
            }

            currentMax = depth % 2 == 0 ? -currentMax : currentMax;

            return dfsValue + currentMax;
        }

        private int MovePiece(Player p1, Player p2, ChessPiece piece, Square square, MoveState moveState)
        {
            int value = 0;

            Square oldSquare = piece.CurrentSquare;

            if (square.IsOccupied)
            {
                ChessPiece p = square.Piece;
                moveState.P2Pieces.Add(new Tuple<ChessPiece, Square, bool>(p, p.CurrentSquare, p.HasMoved));
                value = p.Value;

                p2.PieceSet.Remove(p);
            }
            // specific condition - checks to see if an 'enpassant' move has been made
            else if (piece is Pawn && square.Column != piece.CurrentSquare.Column)
            {
                ChessPiece p = Board.Squares[(piece.CurrentSquare.Row == 3 ? 3 : 4), square.Column].Piece;
                moveState.P2Pieces.Add(new Tuple<ChessPiece, Square, bool>(p, p.CurrentSquare, p.HasMoved));
                value = p.Value;

                p2.PieceSet.Remove(p);
            }

            moveState.P1Pieces.Add(new Tuple<ChessPiece, Square, bool>(piece, piece.CurrentSquare, piece.HasMoved));

            PositionTempPiece(piece, square);

            if (!piece.HasMoved)
            {
                // these features are out for now
                if (piece is Pawn pawn) EnPassantTracker.AnalyseEnpassantConditions(pawn, Board);
                else if (piece is King king)
                {
                    var castle = p1.AnalyseCastlingConditions(king, oldSquare);
                    if (castle != null)
                    {
                        moveState.P1Pieces.Add(new Tuple<ChessPiece, Square, bool>(castle.Item1, castle.Item1.CurrentSquare, false));
                        PositionTempPiece(castle.Item1, castle.Item2);
                        castle.Item1.HasMoved = true;
                    }
                }

                piece.HasMoved = true;
            }

            // also checks if the pawn
            // qualifies for a promotion
            // in its new position
            if (piece is Pawn && (piece.CurrentSquare.Row == 0 || piece.CurrentSquare.Row == 7))
            {
                piece = p1.PromotePawn(piece);
                moveState.PromotedPawn = (Queen)piece;

                if (AnimateThoughts)
                {
                    piece.CurrentSquare.PieceChanged();
                }
            }

            if (EnPassantTracker.HasValue)
            {
                EnPassantTracker.CurrentEnPassants.Clear();
                EnPassantTracker.HasValue = false;
            }

            if (p2.IsCheck(piece))
            {
                value = 20;
                if (p2.IsCheckMate())
                {
                    value = 10000;
                }
            }

            return value;
        }

        protected void PositionTempPiece(ChessPiece piece, Square newSquare)
        {
            piece.CurrentSquare.Piece = null;

            if (AnimateThoughts)
            {
                piece.CurrentSquare.PieceChanged();
            }

            piece.CurrentSquare = newSquare;
            piece.CurrentSquare.Piece = piece;

            if (AnimateThoughts)
            {
                piece.CurrentSquare.PieceChanged();
            }
        }

        private class MoveState
        {
            public Player P1 { get; set; }
            public Player P2 { get; set; }
            public List<Tuple<ChessPiece, Square, bool>> P1Pieces { get; set; }
            public List<Tuple<ChessPiece, Square, bool>> P2Pieces { get; set; }
            public List<Tuple<ChessPiece, Square>> Enpassants { get; set; }
            public Queen PromotedPawn { get; set; }

            public MoveState(Player p1, Player p2)
            {
                P1 = p1;
                P2 = p2;

                P1Pieces = new List<Tuple<ChessPiece, Square, bool>>();
                P2Pieces = new List<Tuple<ChessPiece, Square, bool>>();
                Enpassants = new List<Tuple<ChessPiece, Square>>();

                if (EnPassantTracker.HasValue)
                {
                    foreach (var item in EnPassantTracker.CurrentEnPassants)
                    {
                        Enpassants.Add(new Tuple<ChessPiece, Square>(item.Key, item.Value));
                    }
                }
            }

            public void Restore(bool animateThoughts)
            {
                Square toSquare;
                foreach (var tup in P1Pieces)
                {
                    toSquare = tup.Item1.CurrentSquare;
                    tup.Item1.HasMoved = tup.Item3;
                    tup.Item1.CurrentSquare = tup.Item2;
                    tup.Item2.Piece = tup.Item1;
                    toSquare.Piece = null;

                    if (animateThoughts)
                    {
                        tup.Item2.PieceChanged();
                        toSquare.PieceChanged();
                    }
                }

                foreach (var tup in P2Pieces)
                {
                    tup.Item1.HasMoved = tup.Item3;
                    tup.Item1.CurrentSquare = tup.Item2;
                    tup.Item2.Piece = tup.Item1;

                    P2.PieceSet.Add(tup.Item1);

                    if (animateThoughts)
                    {
                        tup.Item2.PieceChanged();
                    }
                }

                if (Enpassants.Count > 0)
                {
                    foreach (var item in Enpassants)
                    {
                        EnPassantTracker.CurrentEnPassants.Add(item.Item1, item.Item2);
                    }
                }

                if (PromotedPawn != null)
                {
                    P1.PieceSet.Remove(PromotedPawn);
                    P1.PieceSet.Add(P1Pieces[0].Item1);
                }
            }
        }
    }
}
