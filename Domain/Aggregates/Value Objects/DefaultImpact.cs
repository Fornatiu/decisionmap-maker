using System.Text.Json.Serialization;

namespace Domain.Aggregates.Value_Objects
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DefaultImpact
    {
        Immediate,
        Enabling,
        Systemic
    }
}