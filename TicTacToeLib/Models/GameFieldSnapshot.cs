using TicTacToeLib.Enums;

namespace TicTacToeLib.Models;

public class GameFieldSnapshot
{
    private readonly CellValue[,] _state;

    public GameFieldSnapshot(CellValue[,] cellValues)
    {
        _state = cellValues;
    }

    public GameFieldSnapshot()
    {
        _state = new CellValue[3, 3];
    }

    public CellValue[,] GetState()
    {
        return _state;
    }
}