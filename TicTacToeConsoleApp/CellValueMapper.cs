using TicTacToeLib.Enums;

namespace TicTacToeConsoleApp;

public static class CellValueMapper
{
    public static string MapToString(this CellValue cellValue)
    {
        switch (cellValue)
        {
            case CellValue.X: 
                return "X";
            
            case CellValue.O: 
                return "O";
        }

        return " ";
    }
}