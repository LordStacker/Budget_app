using Microsoft.AspNetCore.Mvc;

namespace budget_app.Controllers;

[ApiController]
public class BudgetController : ControllerBase
{
    private readonly ILogger<BudgetController> _logger;

    public BudgetController(ILogger<BudgetController> logger)
    {
        _logger = logger;
    }
}
