using TicTacToeLib.Enums;

namespace TicTacToeLib.Models;

public class GameContext
{
    private readonly GameField _gameField;
    private readonly Stack<GameFieldSnapshot> _history;
    

    public GameContext(GameField gameField)
    {
        _gameField = gameField;
        _history = new Stack<GameFieldSnapshot>();
    }

    public void Backup()
    {
        _history.Push(_gameField.Save());
    }
    
    public void Undo()
    {
        if (_history.TryPop(out var snapshotToRemove))
        {
            if (!_history.TryPeek(out var snapshotToApply))
                snapshotToApply = new GameFieldSnapshot();

            _gameField.Restore(snapshotToApply);
        }
    }

    public void ClearHistory()
    {
        _history.Clear();
    }
    
}