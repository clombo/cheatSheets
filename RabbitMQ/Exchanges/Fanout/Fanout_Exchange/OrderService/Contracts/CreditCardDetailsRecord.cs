namespace Contracts;

public record CreditCardDetailsRecord
{
    public int CardNumber { get; set; }
    public string Expire { get; set; }
    public string CardHolderName { get; set; }
}