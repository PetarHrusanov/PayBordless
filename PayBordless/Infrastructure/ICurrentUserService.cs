namespace PayBordless;

public interface ICurrentUserService
{
    string UserId { get; }
    bool IsAdministrator { get; }
}