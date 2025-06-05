using System.Text.Json.Serialization;

namespace Domain.Aggregates.Value_Objects
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SusteinabilityDimension
    {
        Tech,
        Econ,
        Social,
        Env
    }
}
