namespace service.Models.Command;

public class PostTransaction
{
    public string ItemName { get; set; }
    public int ItemAmount { get; set; }
    public float TotalCost { get; set; }
}