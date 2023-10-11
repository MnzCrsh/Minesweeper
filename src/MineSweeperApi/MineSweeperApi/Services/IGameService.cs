using MineSweeperApi.Entities;

namespace MineSweeperApi.Services;

public interface IGameService
{
    public Game NewGame(int width, int height, int minesCount);
}