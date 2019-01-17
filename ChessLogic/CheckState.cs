using ChessLogic.PieceTypes;

namespace ChessLogic
{
    //this class can also be used to keep track of other match stats
    public class CheckState
    {
        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }

        public King CheckedKing { get; set; }
        public King CheckMateKing { get; set; }

        public string CheckMessage
        {
            get
            {
                if (CheckedKing != null)
                {
                    return $"{CheckedKing.Color}s King is in a check position and must be defended";
                }
                else { return $"There are no current check conditions"; }
            }
        }

        public string CheckMateMessage
        {
            get
            {
                if (CheckMateKing != null)
                {
                    return $"{CheckMateKing.Color}s King is in a check mate postion," +
                             (CheckMateKing.IsWhite ? " Blacks " : " Whites ") + "wins the game";
                }
                else { return "There is no current checkmate conditions"; }
            }
        }
    }
}
