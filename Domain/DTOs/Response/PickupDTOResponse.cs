using Domain.Entities.OrderAggregate;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.Response
{
    public class PickupDTOResponse
    {
        public PickupDTOResponse(Guid id, Guid productId, DateTime startTime, DateTime endTime, PickupStatus status)
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
