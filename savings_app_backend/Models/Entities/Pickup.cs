using savings_app_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models.Entities
{
    public class Pickup
    {
        public Pickup()
        {

        }

        public Pickup(Guid id, Guid productId, DateTime startTime, DateTime endTime, PickupStatus status)
        {
            Id = id;
            ProductId = productId;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(PickupStatus))]
        public PickupStatus Status { get; set; }
    }
}
