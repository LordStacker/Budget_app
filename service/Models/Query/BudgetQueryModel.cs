namespace service.Query;

public class BudgetQueryModel
{
    public required int Id { get; set; }
    public required float StartAmount { get; set; }
    public required float CurrentAmount { get; set; }
}