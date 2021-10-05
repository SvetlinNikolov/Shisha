namespace ShishaProject.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, string> query);

        Task<T> GetAsync<T>(string url);

        Task<T> GetAsync<T>(string url, string language);

        Task<T> PostAsync<T>(string url, string data);

        Task<T> PutAsync<T>(string url, FileStream fs);

        Task<T> PostAsync<T>(string url, Dictionary<string, string> query);

        Task SetTokenAsync();
    }
}
