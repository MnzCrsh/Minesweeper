using System.ComponentModel.DataAnnotations;

namespace MineSweeperApi.Entities;

public class Game
{
    public Guid     GameId     { get; set; }
    [Required, Range(1,30)]
    public int      Width      { get; set; }
    [Required, Range(1,30)]
    public int      Height     { get; set; }
    public int      MinesCount { get; set; }
    [Required]
    public char[][] Field      { get; set; } = null!;
    public bool     Completed  { get; set; }
}