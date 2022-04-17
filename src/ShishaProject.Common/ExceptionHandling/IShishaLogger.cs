namespace ShishaProject.Common.ExceptionHandling
{
    public interface IShishaLogger
    {
        void Information(string message);

        void Warning(string message);

        void Debug(string message);

        void Error(string message);
    }
}
