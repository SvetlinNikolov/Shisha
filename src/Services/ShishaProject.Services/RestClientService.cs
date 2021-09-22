﻿namespace ExampleAPIClient.Client
{
    using System;
    using System.Collections.Generic;
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
        private string baseUri = "http://shisha_project.localhost/api/";

        private TokenResponse token;

        public RestClientService()
        {
            this.token = new TokenResponse();
            this.InitializeTlsProtocol();
        }

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> query)
        {
            await this.SetTokenAsync();

            this.DefaultRequestHeaders.Clear();
            this.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using (HttpResponseMessage response = await this.GetAsync(RestClientUtils.AddQueryString(this.baseUri + url, query)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            await this.SetTokenAsync();

            this.DefaultRequestHeaders.Clear();
            this.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token.AccessToken);

            using (HttpResponseMessage response = await this.GetAsync(this.baseUri + url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> PostAsync<T>(string url, string data)
        {
            await this.SetTokenAsync();

            this.DefaultRequestHeaders.Clear();
            this.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token.AccessToken);

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await this.PostAsync(this.baseUri + url, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> PutAsync<T>(string url, FileStream fs)
        {
            await this.SetTokenAsync();

            this.DefaultRequestHeaders.Clear();
            this.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using (HttpResponseMessage response = await PutAsync(this.baseUri + url, new StreamContent(fs)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        private void InitializeTlsProtocol()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public async Task SetTokenAsync()
        {
            return;
            if (this.token == null || this.token.Expiration > DateTime.Now)
            {
                this.DefaultRequestHeaders.Clear();
                this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "SOME CREDENTIAL SCHEME");

                var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                using (HttpResponseMessage response = await this.PostAsync(this.baseUri + "/some-path-to-get/token", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        this.token = await response.Content.ReadAsAsync<TokenResponse>();
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
        }
    }
}