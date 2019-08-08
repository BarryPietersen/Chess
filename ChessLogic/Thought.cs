using ChessLogic.PieceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    internal class Thought
    {
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public int Value { get; set; }
        public Moved P1Moved { get; set; }
        public Moved P2Moved { get; set; }
        public Moved P1Special { get; set; }
        public Queen PromotedPawn { get; set; }

        public Thought(Player p1, Player p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public void Undo(bool animateThoughts)
        {
            Square toSquare;
            if (P1Moved != null)
            {
                toSquare = P1Moved.Piece.CurrentSquare;
                P1Moved.Piece.HasMoved = P1Moved.HadMoved;
                P1Moved.Piece.CurrentSquare = P1Moved.FromSquare;
                P1Moved.FromSquare.Piece = P1Moved.Piece;
                toSquare.Piece = null;

                if (animateThoughts)
                {
                    P1Moved.FromSquare.PieceChanged();
                    toSquare.PieceChanged();
                }
            }

            if (P1Special != null)
            {
                toSquare = P1Special.Piece.CurrentSquare;
                P1Special.Piece.HasMoved = P1Special.HadMoved;
                P1Special.Piece.CurrentSquare = P1Special.FromSquare;
                P1Special.FromSquare.Piece = P1Special.Piece;
                toSquare.Piece = null;

                if (animateThoughts)
                {
                    P1Special.FromSquare.PieceChanged();
                    toSquare.PieceChanged();
                }
            }

            if (P2Moved != null)
            {
                P2Moved.Piece.HasMoved = P2Moved.HadMoved;
                P2Moved.Piece.CurrentSquare = P2Moved.FromSquare;
                P2Moved.FromSquare.Piece = P2Moved.Piece;

                P2.PieceSet.Add(P2Moved.Piece);

                if (animateThoughts)
                {
                    P2Moved.FromSquare.PieceChanged();
                }
            }

            if (PromotedPawn != null)
            {
                P1.PieceSet.Remove(PromotedPawn);
                P1.PieceSet.Add(P1Moved.Piece);
            }
        }
    }

    internal class Move
    {
        public ChessPiece Piece { get; set; }
        public Square ToSquare { get; set; }
        public int Value { get; set; }

        public Move() { }

        public Move(ChessPiece piece, Square toSquare, int value)
        {
            Piece = piece;
            ToSquare = toSquare;
            Value = value;
        }
    }

    internal class Moved
    {
        public ChessPiece Piece { get; set; }
        public Square FromSquare { get; set; }
        public bool HadMoved { get; set; }

        public Moved() { }

        public Moved(ChessPiece piece, Square fromSquare, bool hadMoved)
        {
            Piece = piece;
            FromSquare = fromSquare;
            HadMoved = hadMoved;
        }
    }
}
