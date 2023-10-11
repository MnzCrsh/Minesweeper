using MineSweeperApi.Entities;

namespace MineSweeperApi.Services;

public class MovementService : IMovementService
{
    public Move MakeMove(Move move, Game currentGame)
    {
        ValidateInputData(move, currentGame);
        
        if (currentGame.Field[move.Row, move.Col] == 'M')
        {
            currentGame.Completed = true;
        }
        
        if (currentGame.Field[move.Row, move.Col] == ' ')
        {
            OpenNeighbouredCells(move, currentGame);
        }
        CheckGameStatus(currentGame);

        return move;
    }

    /// <summary>
    /// Opens every blank cell
    /// </summary>
    /// <param name="move"></param>
    /// <param name="currentGame"></param>
    private void OpenNeighbouredCells(Move move, Game currentGame)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = move.Row + i;
                int newCol = move.Col + j;
                if (newRow >= 0 && newRow < currentGame.Height && newCol >= 0 && newCol < currentGame.Width)
                {
                    if (currentGame.Field[newRow, newCol] == ' ')
                    {
                        //Recursion
                        MakeMove(new Move { GameId = move.GameId, Row = newRow, Col = newCol }, currentGame);
                    }
                }
            }
        }
    }
    
   /// <summary>
   /// 
   /// </summary>
   /// <param name="currentGame"></param>
    private void CheckGameStatus(Game currentGame)
    {
        bool won = true;
        bool lost = false;

        for (int i = 0; i < currentGame.Height && !lost; i++)
        {
            for (int j = 0; j < currentGame.Width && !lost; j++)
            {
                if (currentGame.Field[i, j] == ' ')
                {
                    won = false;
                }
                else if (currentGame.Field[i, j] == 'M')
                {
                    lost = true;
                }
            }
        }

        currentGame.Completed = won || lost;
    }
   
   /// <summary>
   /// 
   /// </summary>
   /// <param name="move"></param>
   /// <param name="currentGame"></param>
   /// <exception cref="Exception"></exception>
    private void ValidateInputData(Move move, Game currentGame)
    {
        if (move.Row < 0 || move.Row >= currentGame.Height || move.Col < 0 || move.Col >= currentGame.Width)
        {
            throw new Exception("Неверные координаты ячейки.");
        }

        if (currentGame.Field[move.Row, move.Col] != ' ')
        {
            throw new Exception("Ячейка уже открыта или содержит мину.");
        }
    }
}