using System.Text.Json.Serialization;

namespace Domain.Aggregates.Value_Objects
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EdgeEffect
    {
        Undefined = 0,
        Positive = 1,
        Negative = -1
    }
}