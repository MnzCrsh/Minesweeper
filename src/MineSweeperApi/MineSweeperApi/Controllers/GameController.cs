using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MineSweeperApi.Entities;
using MineSweeperApi.Services;

namespace MineSweeperApi.Controllers;

[ApiController]
[Route("/api")]
public class GameController : ControllerBase
{
    
    
    private readonly IGameService _game;
    private readonly IMovementService _movement;
    
    private static Game CurrentGame { get; set; } = null!;

    public GameController(IGameService gameService, IMovementService movement)
    {
        _game = gameService;
        _movement = movement;
    }

    [EnableCors("AllowMineSweeper")]
    [HttpPost]
    [Route("/api/new")]
    public IActionResult NewGame([FromBody] Game inputGameData)
    {
        
        try
        {
            var newGame = _game.NewGame(inputGameData.Width, inputGameData.Height, inputGameData.MinesCount);
            CurrentGame = newGame;
        
            return Ok(newGame);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [EnableCors("AllowMineSweeper")]
    [HttpPost]
    [Route("/api/turn")]
    public IActionResult UserTurn(Move move)
    {
        try
        {
            var turn = _movement.MakeMove(move, CurrentGame);
            return Ok(turn);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}