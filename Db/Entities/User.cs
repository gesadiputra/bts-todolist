namespace RecruitmentTest.Db.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string AuthToken { get; set; } = "secret-code";
}