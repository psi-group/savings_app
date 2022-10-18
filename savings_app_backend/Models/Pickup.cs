using savings_app_backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public struct Pickup
{
    public Guid id { get; set; }
    public Guid productId { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(PickupStatus))]
    public PickupStatus status { get; set; }
}