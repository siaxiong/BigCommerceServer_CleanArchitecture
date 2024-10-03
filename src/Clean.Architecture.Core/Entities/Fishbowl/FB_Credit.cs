namespace Clean.Architecture.Core.Entities.Fishbowl;

public class FB_Credit
{
  public FB_Credit(string customerName, double creditAmount)
  {
    CustomerName = customerName;
    CreditAmount = creditAmount;
  }
  public string? CustomerName { get; set; }
  public double CreditAmount { get; set; }
}
