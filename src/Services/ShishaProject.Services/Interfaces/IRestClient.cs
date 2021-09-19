namespace ShishaProject.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, string> query);

        Task<T> GetAsync<T>(string url);

        Task SetTokenAsync();
    }
}
