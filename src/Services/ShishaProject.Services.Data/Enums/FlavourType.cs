namespace ShishaProject.Services.Data.Enums
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum FlavourType
    {
        Unspecified = 0,
        Sweet = 10,
        Mint = 20,
        Strong = 30,
        Fruity = 40,
    }
}
