using ChessLogic.PieceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class PlayerAi : Player
    {
        public int MilliSeconds { get; set; } = 600;
        public bool AnimateThoughts { get; set; }
        public PlayerAi(List<ChessPiece> pieceSet, ChessBoard board,
            CheckState checkState, bool isWhite) : base (pieceSet, board, checkState, isWhite){ }

        public void Think()
        {
            List<Move> moves = EvaluateThoughts();

            // determine the highest value move in moves
            // or if none use a different strategy to analyse
            // the best move
            if (moves.Count > 0)
            {
                var max = moves.Max(m => m.Value);
                var highest = moves.Where(m => m.Value == max).ToList();
                // for now we are just picking a random move from highest
                var move = highest[new Random().Next(0, highest.Count - 1)];

                MovePiece(move.Piece, move.ToSquare);
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

        private List<Move> EvaluateThoughts()
        {
            Thought thought;
            List<Move> moves = new List<Move>();

            foreach (var p in PieceSet.ToList())
            {
                foreach (var sq in ValidateMoves(p))
                {
                    thought = MovePiece(this, Opponent, p, sq);

                    if (AnimateThoughts)
                    {
                        Task.Delay(MilliSeconds).Wait();
                    }

                    thought.Value += DfsThought(Opponent, this);

                    moves.Add(new Move(p, sq, thought.Value));
                    thought.Undo(AnimateThoughts);
                }
            }

            return moves;
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
        // and selecting the highest value move
        private int DfsThought(Player p1, Player p2, int depth = 2)
        {
            if (depth == 0) return 0;

            int currentDfs;
            int dfsValue = 0;
            int currentMax = int.MinValue + 1;

            foreach (var p in p1.PieceSet.ToList())
            {
                foreach (var sq in p1.ValidateMoves(p))
                {
                    Thought thought = MovePiece(p1, p2, p, sq);

                    if (AnimateThoughts)
                    {
                        Task.Delay(MilliSeconds).Wait();
                    }

                    currentDfs = DfsThought(p2, p1, depth - 1);

                    if (thought.Value > currentMax)
                    {
                        currentMax = thought.Value;
                        dfsValue = currentDfs;
                    }

                    thought.Undo(AnimateThoughts);
                }
            }

            // the player does not have any legal moves
            if (currentMax == (int.MinValue + 1))
            {
                return depth % 2 == 0 ? Math.Abs(currentMax) : currentMax;
            }

            currentMax = depth % 2 == 0 ? -currentMax : currentMax;

            return dfsValue + currentMax;
        }

        private Thought MovePiece(Player p1, Player p2, ChessPiece piece, Square toSquare)
        {
            Thought thought = new Thought(p1, p2);

            Square oldSquare = piece.CurrentSquare;

            if (toSquare.IsOccupied)
            {
                ChessPiece p = toSquare.Piece;
                thought.P2Moved = new Moved(p, p.CurrentSquare, p.HasMoved);
                thought.Value = p.Value;

                p2.PieceSet.Remove(p);
            }
            // specific condition - checks to see if an 'enpassant' move has been made
            else if (piece is Pawn && toSquare.Column != piece.CurrentSquare.Column)
            {
                ChessPiece p = Board.Squares[(piece.CurrentSquare.Row == 3 ? 3 : 4), toSquare.Column].Piece;
                thought.P2Moved = new Moved(p, p.CurrentSquare, p.HasMoved);
                thought.Value = p.Value;

                p2.PieceSet.Remove(p);
            }

            thought.P1Moved = new Moved(piece, piece.CurrentSquare, piece.HasMoved);

            PositionTempPiece(piece, toSquare);

            if (!piece.HasMoved)
            {
                // enpassant is out for now
                if (piece is Pawn pawn) EnPassantTracker.AnalyseEnpassantConditions(pawn, Board);
                else if (piece is King king)
                {
                    var castle = p1.AnalyseCastlingConditions(king, oldSquare);
                    if (castle != null)
                    {
                        thought.P1Special = new Moved(castle.Item1, castle.Item1.CurrentSquare, castle.Item1.HasMoved);
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
                thought.PromotedPawn = (Queen)piece;

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

            if (p2.MockIsCheck(piece))
            {
                thought.Value = 20;
                if (p2.IsCheckMate())
                {
                    thought.Value = 10000;
                }
            }

            return thought;
        }

        protected void PositionTempPiece(ChessPiece piece, Square toSquare)
        {
            piece.CurrentSquare.Piece = null;

            if (AnimateThoughts)
            {
                piece.CurrentSquare.PieceChanged();
            }

            piece.CurrentSquare = toSquare;
            piece.CurrentSquare.Piece = piece;

            if (AnimateThoughts)
            {
                piece.CurrentSquare.PieceChanged();
            }
        }
    }
}