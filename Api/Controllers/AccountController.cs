using Microsoft.AspNetCore.Mvc;
using service;
using service.Models.Command;
using service.Services;

namespace budget_app.Controllers;

public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly JwtService _jwtService;
    private readonly ILogger<AccountController> _logger;
    
    public AccountController(AccountService accountService, JwtService jwtService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _jwtService = jwtService;
        _logger = logger;
    }

    [HttpPost]
    [Route("/api/account/login")]
    public IActionResult Login([FromBody] LoginCommandModel model)
    {
        var user = _accountService.Authenticate(model);
        if (user == null) return Unauthorized();
        var token = _jwtService.IssueToken(SessionData.FromUser(user));
        return Ok(new { token });
    }
    [HttpPost]
    [Route("/api/account/register")]
    public IActionResult Register([FromBody] RegisterCommandModel model)
    {
        var user = _accountService.Register(model);
        return Created();
    }
}