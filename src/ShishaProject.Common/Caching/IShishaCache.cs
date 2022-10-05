namespace ShishaProject.Common.Caching
{
    public interface IShishaCache
    {
        public T Get<T>(string key);

        public bool TryGet<T>(string key, out T value);

        public void SetOrUpdate<T>(string key, T value);
    }
}
