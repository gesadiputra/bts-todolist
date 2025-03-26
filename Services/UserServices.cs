using Microsoft.EntityFrameworkCore;
using RecruitmentTest.Db;
using RecruitmentTest.Db.Entities;
using RecruitmentTest.Model.User;

namespace RecruitmentTest.Services;

public interface IUserServices
{
    Task<UserLoginResponse> Login(UserLoginRequest request);
    Task Register(UserRegisterRequest request);
    Task Authorize(string AuthToken);
}
public class UserServices : IUserServices
{
    private readonly ApplicationDbContext _dbContext;
    public UserServices(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserLoginResponse> Login(UserLoginRequest request)
    {
        var user = await _dbContext
            .Set<User>()
            .Where(p => p.Username == request.Username)
            .FirstOrDefaultAsync();
        if(user == null) throw new Exception("User Not Found");
        if (user.Password != request.Password) throw new Exception("Password incorrect");

        //TODO: JWT TOKEN

        return new UserLoginResponse()
        {
            Token = user.AuthToken
        };
    }

    public async Task Register(UserRegisterRequest request)
    {
        var user = new User()
        {
            Email = request.Email,
            Username = request.Username,
            Password = request.Password,
            CreatedDate = DateTime.UtcNow,
        };
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Authorize(string AuthToken)
    {
        var token = await _dbContext
            .Set<User>()
            .Where(p => p.AuthToken == AuthToken)
            .FirstOrDefaultAsync();
        if (token == null) throw new Exception("Invalid Token");
    }
}
