
namespace infrastructure.DataModels;

public class Transaction
{
    public int id { get; set; }
    public string ItemName { get; set; }
    public int ItemAmount { get; set; }
    public float TotalCost { get; set; }
}