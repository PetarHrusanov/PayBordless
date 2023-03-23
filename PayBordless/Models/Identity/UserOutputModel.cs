namespace PayBordless.Models.Identity;

public class UserOutputModel
{
    public UserOutputModel(string token, string user)
    {
        Token = token;
        User = user;
    }

    public string Token { get; }
    public string User { get; set; }
}