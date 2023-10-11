using MineSweeperApi.Entities;

namespace MineSweeperApi.Services;

public class GameService : IGameService
{
    /// <summary>
    /// Creates new game field with unique id
    /// </summary>
    /// <param name="width">Width of the game field</param>
    /// <param name="height">Height of the game field</param>
    /// <param name="minesCount">Count of mines</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Game NewGame(int width, int height, int minesCount)
    {
        ValidateInputData(width, height, minesCount);

        var game = new Game
        {
            GameId = Guid.NewGuid(),
            Width = width,
            Height = height,
            MinesCount = minesCount,
            Completed = false,
            
            Field = new char[height,width]
        };
        
        GenerateField(game.Height, game.Width, game.Field);
        GenerateMines(game.Height, game.Width, game.MinesCount, game.Field);

        return game;
    }

    /// <summary>
    /// Fills game field with closed cells
    /// </summary>
    /// <param name="height">Field height</param>
    /// <param name="width">Field width</param>
    /// <param name="field">Instance of field</param>
    private void GenerateField(int height, int width, char[,] field)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                field[i, j] = ' ';
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="minesCount"></param>
    /// <param name="field"></param>
    private void GenerateMines(int width, int height, int minesCount, char[,] field)
    {
        Random random = new Random();
        for (int i = 0; i < minesCount; i++)
        {
            int row, col;
            do
            {
                row = random.Next(height);
                col = random.Next(width);
            }
            while (field[row, col] == 'M');

            field[row, col] = 'M';
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="minesCount"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void ValidateInputData(int width, int height, int minesCount)
    {
        if (width is 0 || height is 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(width), nameof(height));
        }

        if (minesCount > width * height - 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(minesCount));
        }
    }
}