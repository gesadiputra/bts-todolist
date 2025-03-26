using Microsoft.AspNetCore.Mvc;
using RecruitmentTest.Model.User;
using RecruitmentTest.Services;

namespace RecruitmentTest.Controllers;

[ApiController]
[Route("")]
public class UserController : ControllerBase
{
    private readonly IUserServices _user;
    public UserController(IUserServices user)
    {
        _user = user;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var result = await _user.Login(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request)
    {
        await _user.Register(request);
        return Ok();
    }
}
