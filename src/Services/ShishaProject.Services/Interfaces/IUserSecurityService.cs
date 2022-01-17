namespace ShishaProject.Services.Interfaces
{
    public interface IUserSecurityService
    {
        string EncryptPassword(string inputString);
    }
}
