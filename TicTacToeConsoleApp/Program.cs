using TicTacToeConsoleApp;
using TicTacToeLib.Enums;
using TicTacToeLib.Models;


var gameField = new GameField();
var gameContext = new GameContext(gameField);

var currentMark = CellValue.O;
var cursor = new int[2] { 0, 0 };

gameField.Won += (sender, eventArgs) => EndGame($"Player {eventArgs.Mark} has won");
gameField.Draw += () => EndGame("Draw");

Start();

void Start()
{
    while (true)
    {
        RenderGameField();
        ReadKey();
    }
}

void MakeAMove()
{
    if (gameField[cursor[0], cursor[1]] == CellValue.Empty)
    {
        gameField.MakeAMove(new MoveCommand(cursor[0], cursor[1], currentMark));
        gameContext.Backup();
        switchCurrentMark();
        RenderGameField();
    }
}

void Undo()
{
    gameContext.Undo();
    switchCurrentMark();
    RenderGameField();
}

void RenderGameField()
{
    Console.Clear();
    for (int x = 0; x < 3; x++)
    {
        if (x == 1 || x == 2)
            Console.WriteLine("---+---+---");
        Console.WriteLine($" {gameField[x,0].MapToString()} | " +
                          $"{gameField[x,1].MapToString()} | {gameField[x,2].MapToString()} ");
    }
    RenderCursor();
}

void RenderCursor()
{
    Console.SetCursorPosition(cursor[1]*4 + 1, cursor[0]*2);
}

void EndGame(string message)
{
    Console.Clear();
    Console.WriteLine($"{message}! Tap any key to restart");
    Console.ReadKey();
    gameField?.Clear();
    gameContext?.ClearHistory();
    Start();
}

void ReadKey()
{
    var key = Console.ReadKey().Key;
    switch (key)
    {
        case ConsoleKey.UpArrow:
            Up();
            break;
        case ConsoleKey.DownArrow:
            Down();
            break;
        case ConsoleKey.LeftArrow:
            Left();
            break;
        case ConsoleKey.RightArrow:
            Right();
            break;
        case ConsoleKey.Enter:
            MakeAMove();
            break;
        case ConsoleKey.Backspace: Undo();
            break;
    }
}

void Up()
{
    if (cursor[0] > 0)
    {
        cursor[0] -= 1;
        RenderCursor();
    }
}
void Down()
{
    if (cursor[0] < 2)
    {
        cursor[0] += 1;
        RenderCursor();
    }
}

void Left()
{
    if (cursor[1] > 0)
    {
        cursor[1] -= 1;
        RenderCursor();
    }
}

void Right()
{
    if (cursor[1] < 2)
    {
        cursor[1] += 1;
        RenderCursor();
    }
}

void switchCurrentMark()
{
    currentMark = currentMark == CellValue.X ? CellValue.O : CellValue.X;
}