namespace ShishaProject.Common.Helpers
{
    using System.Collections.Generic;
    using System.Dynamic;

    using Newtonsoft.Json;

    public static class JsonHelper
    {
        public static string SerializeToPhpApiFormat<T>(string name, T obj)
        {
            var expandoObject = new ExpandoObject();
            expandoObject.TryAdd(name, obj);

            var result = JsonConvert.SerializeObject(expandoObject);

            return result;
        }
    }
}
