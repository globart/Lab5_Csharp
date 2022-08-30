using TicTacToeLib.Enums;

namespace TicTacToeLib.EventArgs;

public class WinEventArgs : System.EventArgs
{
    public CellValue Mark { get; set; }
}