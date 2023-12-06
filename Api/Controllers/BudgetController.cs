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

    public BudgetController(AccountService accountService, JwtService jwtService, BudgetService budgetService, ILogger<BudgetController> logger)
    {
        _accountService = accountService;
        _jwtService = jwtService;
        _budgetService = budgetService;
        _logger = logger;
    }

    [HttpPost]
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
                return Unauthorized();
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
    public IActionResult UpdateCurrentAmount([FromHeader(Name = "Authorization")] string authorizationHeader, [FromBody] UpdateCurrentAmountCommand command)
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
                return Unauthorized();
            }
        }else{
            return Unauthorized();
        }
        
        var user = _accountService.Get(sessionData);
        if (user == null) return Unauthorized();
        return Ok(_budgetService.UpdateCurrentAmount(user.Id, command.NewCurrentAmount));
        //return Ok("tested");
    }

    [HttpPost]
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
                return Unauthorized();
            }
        }else{
            return Unauthorized();
        }
        
        var user = _accountService.Get(sessionData);
        if (user == null) return Unauthorized();
        return Ok(_budgetService.GetStartAmount(user.Id) .StartAmount);
        //return Ok("tested");
    }
    

    
    [HttpPost]
    [Route("/api/update-total-amount")]
    public IActionResult UpdateStartAmount([FromHeader(Name = "Authorization")] string authorizationHeader, [FromBody] UpdateStartAmountCommand command) //[FromBody] float updatedStartAmount)
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
                return Unauthorized();
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
        

        try
        {
             _budgetService.UpdateStartAmount(user.Id, command.updatedStartAmount);
        }
        catch (Exception ex)
        {
             return BadRequest("Failed to update your total amount.");
        }

        return Ok("Your total amount was updated successfully."); 
    }




}