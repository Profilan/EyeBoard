namespace EyeBoard.Logic.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
