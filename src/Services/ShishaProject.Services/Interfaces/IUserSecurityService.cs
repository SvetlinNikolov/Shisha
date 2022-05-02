namespace ShishaProject.Services.Interfaces
{
    public interface IUserSecurityService
    {
        (string HashedPassword, string Salt) EncryptPassword(string inputString, string inputSalt = null);
    }
}
