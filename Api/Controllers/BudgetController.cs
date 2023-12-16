using System.Net;
using infrastructure.DataModels;
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
    private SessionData sessionData;

    public BudgetController(AccountService accountService, JwtService jwtService, BudgetService budgetService,
        ILogger<BudgetController> logger)
    {
        _accountService = accountService;
        _jwtService = jwtService;
        _budgetService = budgetService;
        _logger = logger;
    }

    [HttpGet]
    [Route("/api/get-current-amount")]
    public IActionResult GetCurrentAmount([FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            string token = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                sessionData = _jwtService.ValidateAndDecodeToken(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }
        else
        {
            return Unauthorized();
        }

        var user = _accountService.Get(sessionData);
        if (user == null) return Unauthorized();
        return Ok(_budgetService.GetCurrentAmount(user.Id).CurrentAmount);
    }

    [HttpPost]
    [Route("/api/update-current-amount")]
    public IActionResult UpdateCurrentAmount([FromHeader(Name = "Authorization")] string authorizationHeader,
        [FromBody] UpdateCurrentAmountCommand command)
    {
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            string token = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                sessionData = _jwtService.ValidateAndDecodeToken(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }
        else
        {
            return Unauthorized();
        }

        var user = _accountService.Get(sessionData);
        if (user == null) return Unauthorized();
        var updatedBudget = _budgetService.UpdateCurrentAmount(user.Id, command.NewCurrentAmount);
        return Ok(updatedBudget);

    }

    [HttpGet]
    [Route("/api/total-amount")]
    public IActionResult GetStartAmount([FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            string token = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                sessionData = _jwtService.ValidateAndDecodeToken(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }
        else
        {
            return Unauthorized();
        }

        var user = _accountService.Get(sessionData);
        if (user == null) return Unauthorized();
        return Ok(_budgetService.GetStartAmount(user.Id).StartAmount);
    }



    [HttpPut]
    [Route("/api/update-total-amount")]
    public IActionResult UpdateStartAmount([FromHeader(Name = "Authorization")] string authorizationHeader,
        [FromBody] UpdateStartAmountCommand command)
    {

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            string token = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                sessionData = _jwtService.ValidateAndDecodeToken(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }

        else
        {
            return Unauthorized();
        }

        var user = _accountService.Get(sessionData);
        if (user == null)
        {
            return Unauthorized();
        }

        var updateStartAmount = _budgetService.UpdateStartAmount(user.Id, command.updatedStartAmount);
        return Ok(updateStartAmount);
    }

    [HttpGet]
    [Route("/transactions")]
    public IActionResult getTransactions([FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            string token = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                sessionData = _jwtService.ValidateAndDecodeToken(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }

        else
        {
            return Unauthorized();
        }
        var user = _accountService.Get(sessionData);
        if (user == null)
        {
            return Unauthorized();
        }
        var transactions = _budgetService.getTransactions(user.Id);
        Console.WriteLine(transactions);
        return Ok(transactions);
}
    [HttpPost]
    [Route("/post/transactions")]
    public IActionResult PostTransactions([FromHeader(Name = "Authorization")] string authorizationHeader,
        [FromBody] PostTransaction command)
    {
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            string token = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                sessionData = _jwtService.ValidateAndDecodeToken(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e);
            }
        }

        else
        {
            return Unauthorized();
        }
        var user = _accountService.Get(sessionData);
        if (user == null)
        {
            return Unauthorized();
        }
        var transactions = _budgetService.PostTransactions(user.Id, command);
        return Ok(transactions);
    }


}