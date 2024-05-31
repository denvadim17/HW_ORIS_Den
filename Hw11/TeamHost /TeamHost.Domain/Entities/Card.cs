namespace TeamHost.Domain.Entities;

public class Card
{
    public int CardId { get; set; }

    public string CardNumber { get; set; }

    public decimal Balance { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }
}