using Microsoft.AspNetCore.Mvc;
using service;
using service.Models.Command;
using service.Services;

namespace budget_app.Controllers;

[ApiController]
public class BudgetController : ControllerBase
{
    private readonly BudgetService _budgetService;
    private readonly AccountService _accountService;
    private readonly JwtService _jwtService;
    private readonly ILogger<BudgetController> _logger;

    public BudgetController(AccountService accountService, JwtService jwtService, BudgetService budgetService)
    {
        _accountService = accountService;
        _jwtService = jwtService;
        _budgetService = budgetService;
    }

    [HttpPost]
    [Route("/api/current-amount")]
    public IActionResult GetCurrentAmount([FromBody] string token)
    {
        var sessionData = _jwtService.ValidateAndDecodeToken(token);
        var user = _accountService.Get(sessionData);
        if (user == null) return Unauthorized();
        return Ok(_budgetService.GetCurrentAmount(user.Id).CurrentAmount);

    }
}