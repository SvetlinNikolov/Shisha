namespace ShishaProject.Common.ExtensionMethods
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public static class HttpExtensions
    {
        public static async Task<Tout> ReadAsAsync<Tout>(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<Tout>(await content.ReadAsStringAsync());
        }
    }
}
