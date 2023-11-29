using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Budget
{
    [Required]
    public int Id { get; set; }
    [Required]
    public float StartAmount { get; set; }
    [Required]
    public float CurrentAmount { get; set; }
}