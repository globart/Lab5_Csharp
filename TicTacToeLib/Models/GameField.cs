using TicTacToeLib.Enums;
using TicTacToeLib.EventArgs;

namespace TicTacToeLib.Models;

public class GameField
{
    private CellValue[,] _state;

    public GameField()
    {
        _state = new CellValue[3,3];
    }

    public event EventHandler<WinEventArgs> Won;
    public event Action Draw;
    
    public CellValue this[int x, int y]
    {
        get => _state[x, y];
    }
    
    public bool MakeAMove(MoveCommand moveCommand)
    {
        if (_state[moveCommand.X, moveCommand.Y] == CellValue.Empty)
        {
            _state[moveCommand.X, moveCommand.Y] = moveCommand.CellValue;
            CheckResult(moveCommand);
            return true;
        }

        return false;
    } 
    
    public GameFieldSnapshot Save()
    {
        var stateCopy = _state.Clone() as CellValue[,];
        return new GameFieldSnapshot(stateCopy!);
    }

    public void Restore(GameFieldSnapshot snapshot)
    {
        var stateCopy = snapshot.GetState().Clone() as CellValue[,];
        _state = stateCopy!;
    }

    public void Clear()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                _state[x, y] = CellValue.Empty;
            }
        }
    }

    private void CheckResult(MoveCommand moveCommand)
    {
        var x = moveCommand.X;
        var y = moveCommand.Y;
        
        CellValue[] horizontal = new[] { _state[0, y], _state[1, y], _state[2, y] };
        CellValue[] vertical = new [] { _state[x, 0], _state[x, 1], _state[x, 2] };
        CellValue[] diagonal = new[] { _state[0, 2], _state[1, 1], _state[2, 0] };
        CellValue[] backDiagonal = new[] { _state[0, 0], _state[1, 1], _state[2, 2] };

        var isWin = horizontal.All(c => c == horizontal[0] && horizontal[0] != CellValue.Empty)
                    || vertical.All(c => c == vertical[0] && vertical[0] != CellValue.Empty)
                    || diagonal.All(c => c == diagonal[0] && diagonal[0] != CellValue.Empty)
                    || backDiagonal.All(c => c == backDiagonal[0] && backDiagonal[0] != CellValue.Empty);
        if (isWin)
            OnWin(new WinEventArgs(){ Mark = moveCommand.CellValue});

        var areAllCellsFilled = (from CellValue cell in _state select cell)
            .All(c => c != CellValue.Empty);
        
        if (!isWin && areAllCellsFilled)
            OnDraw();
    }

    private void OnWin(WinEventArgs args)
    {
        var handler = Won;
        handler?.Invoke(this, args);
    }
    
    private void OnDraw()
    {
        var handler = Draw;
        handler?.Invoke();
    }
}