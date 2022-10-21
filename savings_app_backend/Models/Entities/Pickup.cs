using savings_app_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models.Entities
{
    public class Pickup
    {
        public Guid Id { get; set; }
        public Guid productId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(PickupStatus))]
        public PickupStatus status { get; set; }
    }
}
