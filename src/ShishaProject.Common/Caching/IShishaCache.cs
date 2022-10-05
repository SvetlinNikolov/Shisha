namespace ShishaProject.Common.Caching
{
    public interface IShishaCache
    {
        public T Get<T>(string key);

        public void Set<T>(string key, T value);
    }
}
