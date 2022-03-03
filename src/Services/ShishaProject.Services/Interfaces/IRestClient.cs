namespace ShishaProject.Services.Interfaces
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, string> query);

        Task<T> GetAsync<T>(string url);

        Task<T> GetAsync<T>(string url, string language);

        Task<T> PostAsync<T>(string url, string data);

        Task<T> PutAsync<T>(string url, FileStream fs);

        Task<T> PutAsync<T>(string url, string data);

        Task<T> PostAsync<T>(string url, Dictionary<string, string> query);
    }
}
