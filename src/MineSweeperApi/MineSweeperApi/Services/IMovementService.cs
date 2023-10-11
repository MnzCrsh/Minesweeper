using MineSweeperApi.Entities;

namespace MineSweeperApi.Services;

public interface IMovementService
{
    public Move MakeMove(Move move, Game currentGame);
}