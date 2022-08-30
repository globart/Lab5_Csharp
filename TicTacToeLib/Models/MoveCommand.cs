using System.Reflection.PortableExecutable;
using TicTacToeLib.Enums;

namespace TicTacToeLib.Models;

public class MoveCommand
{
    public CellValue CellValue { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public MoveCommand(int x, int y, CellValue cellValue)
    {
        (X, Y) = (x, y);
        CellValue = cellValue;
    }
}