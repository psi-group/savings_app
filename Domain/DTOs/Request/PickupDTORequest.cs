using Domain.Entities.OrderAggregate;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.DTOs.Request
{
    public class PickupDTORequest
    {
        [Required]
        public Guid? ProductId { get; set; }
        [Required]
        public DateTime? StartTime { get; set; }
        [Required]
        public DateTime? EndTime { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(PickupStatus))]
        public PickupStatus? Status { get; set; } = PickupStatus.Available;
    }
}