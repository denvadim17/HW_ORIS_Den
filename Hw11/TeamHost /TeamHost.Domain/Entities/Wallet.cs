namespace TeamHost.Domain.Entities;

public class Wallet
{
    public int WalletId { get; set; }

    public decimal Balance { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public List<Transaction> Transactions { get; set; }
}