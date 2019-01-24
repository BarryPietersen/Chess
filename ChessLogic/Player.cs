using ChessLogic.PieceTypes;
using System;
using System.Collections.Generic;

namespace ChessLogic
{
    /// <summary>
    /// Represents a chess player
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The current set of pieces belonging to the player, captured pieces will be removed from this list.
        /// </summary>
        public List<ChessPiece> PieceSet { get; }

        /// <summary>
        /// Represents the opponent player and all pieces belonging to the opposition.
        /// </summary>
        public Player Opponent { get; set; }

        /// <summary>
        /// The current chess board with the populated squares matrix. All decisions and observable moves made by the player will take effect and be based on the state of this board.
        /// </summary>
        public ChessBoard Board { get; }

        /// <summary>
        /// A shared object which stores information on any check/checkmate conditions.
        /// </summary>
        private CheckState CheckState;

        /// <summary>
        /// Returns a bool representing the players color.
        /// </summary>
        public bool IsWhite { get; }

        /// <summary>
        /// Returns a string representing the players color.
        /// </summary>
        public string Color => IsWhite ? "white" : "black";

        /// <summary>
        /// Initialize a new instance of a Player with the specified properties. 
        /// </summary>
        /// <param name="pieceSet">The List of chess pieces the player will manage in the game.</param>
        /// <param name="board">The current chess board with the populated squares matrix.</param>
        /// <param name="checkState">The check state manager shared by both players and the game.</param>
        /// <param name="isWhite">Indicates the players color, this must match the chess pieces color.</param>
        public Player(List<ChessPiece> pieceSet, ChessBoard board,
                      CheckState checkState, bool isWhite)
        {
            PieceSet = pieceSet;
            Board = board;
            CheckState = checkState;
            IsWhite = isWhite;
        }

        // moves a players ChessPiece to a new square on the Board,
        // handles capture and clean up if a piece exists on the square,
        // invokes members responsible for analysing the states of special 
        // conditions relating to castling and en passant special moves,
        // calls IsCheck/IsCheckMate returns true if Check Mate is discovered

        /// <summary>
        /// Moves a players chess piece to a new square, if a piece exists on the new square that piece is captured. Observes the status of special conditions. Raises check/checkmate events.
        /// </summary>
        /// <param name="piece">The piece to be moved.</param>
        /// <param name="newSquare">The new square the piece will be positioned on.</param>
        /// <returns>Returns true if a checkmate condition has occurred.</returns>
        public bool MovePiece(ChessPiece piece, Square newSquare)
        {
            if (newSquare.IsOccupied) CapturePiece(newSquare.Piece);
            // specific condition - checks to see if an 'enpassant' move has been made
            else if (piece is Pawn && newSquare.Column != piece.CurrentSquare.Column)
                CapturePiece(Board.Squares[(piece.CurrentSquare.Row == 3 ? 3 : 4), newSquare.Column].Piece);

            PositionPiece(piece, newSquare);

            if (!piece.HasMoved)
            {
                if (piece is Pawn pawn) EnPassantTracker.AnalyseEnpassantConditions(pawn, Board);
                else if (piece is King king) AnalyseCastlingConditions(king);

                piece.HasMoved = true;
            }

            // also checks if the pawn
            // qualifies for a promotion
            // in its new position
            if (piece is Pawn promPawn && (promPawn.CurrentSquare.Row == 0 ||
                                           promPawn.CurrentSquare.Row == 7 ))
            {
                piece = PromotePawn(piece);
            }

            if (EnPassantTracker.HasValue)
            {
                EnPassantTracker.CurrentEnPassants.Clear();
                EnPassantTracker.HasValue = false;
            }

            return IsCheck(piece) && IsCheckMate();
        }

        // creates temporary changes to the state of the board object.
        // performs exhaustive searches across the squares matrix,
        // further qualifies possible moves and returns truly legal moves,
        // it is to ensure the king never becomes exposed to a check position
        // by accidentally moving a piece that is securing the line of fire.

        /// <summary>
        /// Exhaustively searches a pieces possible moves collection, ignores any moves which might result in a compromise to the kings security or line of fire.
        /// Creates temporary changes to the state of the board object.
        /// </summary>
        /// <param name="piece">The piece of interest, the 'possible moves' collection to be validated.</param>
        /// <returns>Returns a List of valid sqaures that the current piece could move to. Considers maintaining the kings defence line of fire.</returns>
        public List<Square> ValidateMoves(ChessPiece piece)
        {
            List<Square> validMoves = piece.SearchPossibleMoves(Board);
            Square[] possibleMoves = validMoves.ToArray();
            Square homeSquare = piece.CurrentSquare;

            foreach (Square sq in possibleMoves)
            {
                if (sq.IsOccupied)
                {
                    // the square contians a piece of the oppenet set,
                    // extra care should be taken to ensure the piece is
                    // assumed to be captured and removed before IsCheck
                    // is called, to ensure true results of valid moves

                    // remove and preserve opponent piece
                    ChessPiece tempPiece = sq.Piece;
                    Opponent.PieceSet.Remove(tempPiece);

                    CreateMockMove(piece, sq);

                    foreach (ChessPiece opPiece in Opponent.PieceSet)
                    {
                        // IsCheck every opponent piece
                        if (MockIsCheck(opPiece))
                        {
                            validMoves.Remove(sq);
                            break;
                        }
                    }
                    ReinstateMockMove(homeSquare, sq);

                    // reinstate opponent piece
                    Opponent.PieceSet.Add(tempPiece);
                    sq.Piece = tempPiece;
                }
                else
                {
                    // create mock move to an empty square
                    CreateMockMove(piece, sq);

                    foreach (ChessPiece opPiece in Opponent.PieceSet)
                    {
                        // IsCheck every opponent piece
                        if (MockIsCheck(opPiece))
                        {
                            // remove sq from collection
                            validMoves.Remove(sq);
                            break;
                        }
                    }
                    ReinstateMockMove(homeSquare, sq);
                }
            }
            return validMoves;
        }

        private void CreateMockMove(ChessPiece piece, Square tempSquare)
        {
            Square currentSquare = piece.CurrentSquare;
            currentSquare.Piece = null;
            piece.CurrentSquare = tempSquare;
            piece.CurrentSquare.Piece = piece;
        }

        private void ReinstateMockMove(Square homeSquare, Square tempSquare)
        {
            ChessPiece piece = tempSquare.Piece;

            homeSquare.Piece = piece;
            piece.CurrentSquare = homeSquare;
            tempSquare.Piece = null;
        }

        private bool MockIsCheck(ChessPiece attacker)
        {
            foreach (Square sq in attacker.SearchPossibleMoves(Board))
            {
                if (sq.IsOccupied && sq.Piece is King)
                {
                    return true;
                }
            }
            return false;
        }

        // call searchpossiblemoves on the attacking piece
        // to see if any square returned has a piece
        // property that points to the king
        private bool IsCheck(ChessPiece attacker)
        {
            foreach (Square sq in attacker.SearchPossibleMoves(Board))
            {
                if (sq.IsOccupied && sq.Piece is King king)
                {
                    CheckState.CheckedKing = king;
                    CheckState.IsCheck = true;
                    return true;
                }
            }
            return false;
        }

        private bool IsCheckMate()
        {
            foreach (ChessPiece piece in Opponent.PieceSet)
            {
                if (Opponent.ValidateMoves(piece).Count > 0)
                {
                    return false;
                }
            }

            //exhaustive search has failed, return true
            CheckState.CheckMateKing = CheckState.CheckedKing;
            CheckState.IsCheckMate = true;
            return true;
        }

        // destroys all references to the piece
        private void CapturePiece(ChessPiece piece)
        {
            piece.CurrentSquare.Piece = null;
            Opponent.PieceSet.Remove(piece);
        }

        private void PositionPiece(ChessPiece piece, Square newSquare)
        {
            piece.CurrentSquare.Piece = null;
            piece.CurrentSquare = newSquare;
            piece.CurrentSquare.Piece = piece;
        }

        // promotes a pawn piece to a queen piece,
        // call this method after the pawn has been
        // positioned in its new square to check
        // if it qualifies for a promotion
        private ChessPiece PromotePawn(ChessPiece piece)
        {
            PieceSet.Remove(piece);
            piece = new Queen(piece.IsWhite, piece.CurrentSquare);
            piece.CurrentSquare.Piece = piece;
            PieceSet.Add(piece);
            return piece;
        }

        // performs the special 'castling' move and
        // positions the rook in its new square
        private void AnalyseCastlingConditions(King king)
        {
            if (king.CurrentSquare.Row == 0 || king.CurrentSquare.Row == 7)
            {
                if (king.CurrentSquare.Column == 6 || king.CurrentSquare.Column == 5)
                {
                    Rook rook = (Rook)Board.Squares[king.CurrentSquare.Row, 7].Piece;
                    MovePiece(rook, Board.Squares[king.CurrentSquare.Row, king.CurrentSquare.Column - 1]);
                }
                else if (king.CurrentSquare.Column == 2 || king.CurrentSquare.Column == 1)
                {
                    Rook rook = (Rook)Board.Squares[king.CurrentSquare.Row, 0].Piece;
                    MovePiece(rook, Board.Squares[king.CurrentSquare.Row, king.CurrentSquare.Column + 1]);
                }
            }
        }
    }
}
