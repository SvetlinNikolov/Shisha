namespace ExampleAPIClient.Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using ShishaProject.Common.ExtensionMethods;
    using ShishaProject.Common.Utils;
    using ShishaProject.Services.Interfaces;

    public class RestClientService : HttpClient, IRestClient
    {
        private readonly IJwtService jwtService;
        private string baseUri;

        public RestClientService(IJwtService jwtService)
        {
            this.InitializeTlsProtocol();
            this.baseUri = "http://shisha_project.localhost/api/";
            this.jwtService = jwtService;
        }

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> query)
        {
            this.SetToken();

            using (HttpResponseMessage response = await this.GetAsync(RestClientUtils.AddQueryString(this.baseUri + url, query)))
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
            this.SetToken();

            using (HttpResponseMessage response = await this.GetAsync(this.baseUri + url + RestClientUtils.AddQueryLanguage(language)))
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
            this.SetToken();

            using (HttpResponseMessage response = await this.GetAsync(this.baseUri + url))
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
            this.SetToken();
            var content = new StringContent(data, Encoding.Default, "application/json");

            using (HttpResponseMessage response = await this.PostAsync(this.baseUri + url, content))
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
            this.SetToken();

            var content = new StringContent(RestClientUtils.AddQueryString(this.baseUri + url, query), Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await this.PostAsync(this.baseUri + url, content))
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
            this.SetToken();

            using (HttpResponseMessage response = await this.PutAsync(this.baseUri + url, new StreamContent(fs)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.Validate().ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        private void InitializeTlsProtocol()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        private void SetToken()
        {
            this.DefaultRequestHeaders.Clear();
            this.DefaultRequestHeaders.Add("jwt_token", this.jwtService.GenerateToken());
        }
    }
}