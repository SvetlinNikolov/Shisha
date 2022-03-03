namespace ShishaProject.Services.Interfaces
{
    public interface IUserSecurityService
    {
        (string Password, string Salt) EncryptPassword(string inputString);
    }
}
