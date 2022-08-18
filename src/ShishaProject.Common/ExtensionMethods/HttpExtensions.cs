namespace ShishaProject.Common.ExtensionMethods
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public static class HttpExtensions
    {
        public static async Task<Tout> ReadAsAsync<Tout>(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<Tout>(await content.ReadAsStringAsync());
        }

        public static HttpContent Validate(this HttpContent content)
        {
            var message = JsonConvert.DeserializeObject<HttpMessage>(content.ReadAsStringAsync().GetAwaiter().GetResult());

            if (message.StatusCode != (int)HttpStatusCode.OK)
            {
                if (message.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    return content;
                }

                throw new InvalidOperationException(FormatExceptionMessage(message));
            }

            return content;
        }

        private static string FormatExceptionMessage(HttpMessage message)
        {
            return $"Status Code {message.StatusCode} {Environment.NewLine} {string.Join(Environment.NewLine, message.Errors.Split("|"))}";
        }
    }

    public class HttpMessage
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("error_message")]
        public string Errors { get; set; }
    }
}