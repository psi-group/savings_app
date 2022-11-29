using Domain.Enums;
using Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Pickup : BaseEntity, IAggregateRoot
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
        
        public Guid ProductId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(PickupStatus))]
        public PickupStatus Status { get; private set; }

        public void Book()
        {
            Status = PickupStatus.Taken;
        }
    }
}
