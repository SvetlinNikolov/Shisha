namespace ExampleAPIClient.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using ShishaProject.Common.Constants;
    using ShishaProject.Common.ExtensionMethods;
    using ShishaProject.Common.Utils;
    using ShishaProject.Services.Interfaces;

    public class RestClientService : IRestClient
    {
        private readonly IJwtService jwtService;
        private readonly IHttpClientFactory httpClientFactory;

        public RestClientService(IJwtService jwtService, IHttpClientFactory httpClientFactory)
        {
            this.jwtService = jwtService;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> query)
        {
            var httpClient = this.CreateHttpClient();

            using (HttpResponseMessage response = await httpClient.GetAsync(RestClientUtils.AddQueryString(EndpointConstants.BaseUri + url, query)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.Validate().ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> GetAsync<T>(string url, string language)
        {
            var httpClient = this.CreateHttpClient();

            using (HttpResponseMessage response = await httpClient.GetAsync(EndpointConstants.BaseUri + url + RestClientUtils.AddQueryLanguage(language)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var test = await response.Content.ReadAsAsync<dynamic>();
                    return await response.Content.Validate().ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var httpClient = this.CreateHttpClient();

            using (HttpResponseMessage response = await httpClient.GetAsync(EndpointConstants.BaseUri + url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var test = await response.Content.ReadAsAsync<dynamic>();
                    return await response.Content.Validate().ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> PostAsync<T>(string url, string data)
        {
            var httpClient = this.CreateHttpClient();
            var content = new StringContent(data, Encoding.Default, "application/json");

            using (HttpResponseMessage response = await httpClient.PostAsync(EndpointConstants.BaseUri + url, content))
            {
                var test = await response.Content.ReadAsAsync<dynamic>();

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.Validate().ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> PostAsync<T>(string url, Dictionary<string, string> query)
        {
            var httpClient = this.CreateHttpClient();

            var content = new StringContent(RestClientUtils.AddQueryString(EndpointConstants.BaseUri + url, query), Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await httpClient.PostAsync(EndpointConstants.BaseUri + url, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.Validate().ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> PutAsync<T>(string url, FileStream fs)
        {
            var httpClient = this.CreateHttpClient();

            using (HttpResponseMessage response = await httpClient.PutAsync(EndpointConstants.BaseUri + url, new StreamContent(fs)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.Validate().ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        private void SetJwtToken(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("jwt_token", this.jwtService.GenerateToken());
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = this.httpClientFactory.CreateClient();
            this.SetJwtToken(httpClient);

            return httpClient;
        }
    }
}
