namespace MineSweeperApi.Entities;

public class Move
{
    public Guid GameId { get; set; }
    public int  Row    { get; set; }
    public int  Col    { get; set; }
}