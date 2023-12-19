using System.Net.Mime;
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
    private SessionData sessionData;
    
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
        try
        {
            var user = _accountService.Authenticate(model);
            if (user == null) return Unauthorized();
            var token = _jwtService.IssueToken(SessionData.FromUser(user));
            return Ok(new { token });
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred during login: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during login.");
        }
    }
    [HttpPost]
    [Route("/api/account/register")]
    public IActionResult Register([FromBody] RegisterCommandModel model)
    {
        try
        {
            var user = _accountService.Register(model);
            return Created("/api/account/user/" + user.Id, user);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred during registration: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during registration.");
        }
    }

    [HttpGet]
    [Route("/api/account/me")]
    public IActionResult getMe([FromHeader(Name = "Authorization")] string authorizationHeader)
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
        return Ok(user);
    }
    
    [HttpPut]
    [Route("api/account/update/user")]
    public IActionResult EditUser([FromHeader(Name = "Authorization")] string authorizationHeader, [FromBody] UpdateUserCommandModel model)
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
        var updatedUser = _accountService.UpdateUser(user.Id,model);
        return Ok(updatedUser);
    }
    
    [HttpPut]
    [Route("api/account/edit/password")]
    public IActionResult EditMail([FromHeader(Name = "Authorization")] string authorizationHeader, [FromBody] EditPasswordCommandModel model)
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
        
        LoginCommandModel loginModel = new LoginCommandModel();
        loginModel.Email = model.UserEmail;
        loginModel.Password = model.OldPassword;
        
        var user = _accountService.Authenticate(loginModel);
        
        if (user == null) return Unauthorized();

        var updatedUser = _accountService.UpdatePassword(user.Id, model.NewPassword);
        return Ok(updatedUser);
    }

    [HttpPost]
    [Route("api/account/upload/image")]
    public IActionResult UploadImage([FromHeader(Name = "Authorization")] string authorizationHeader,
        [FromBody] UploadPhotoCommandModel image)
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
        var updatedUser = _accountService.UpdateProfilePhoto(user.Id, image.image);
        return Ok(updatedUser);
    }
    
    [HttpDelete]
    [Route("api/account/delete")]
    public IActionResult DeleteUser([FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        try
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
            _accountService.DeleteUser(user.Id);
            return Ok("User deleted");
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred during user deletion: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during user deletion.");
        }
    }
    

}