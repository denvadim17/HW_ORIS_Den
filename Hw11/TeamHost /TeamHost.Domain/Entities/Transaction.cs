namespace TeamHost.Domain.Entities;

public class Transaction
{
    public int TransactionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Type { get; set; }

    public string Description { get; set; }

    public int WalletId { get; set; }

    public Wallet Wallet { get; set; }
}