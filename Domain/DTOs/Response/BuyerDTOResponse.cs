using Domain.Entities.OrderAggregate;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Response
{
    public class BuyerDTOResponse
    {
        public BuyerDTOResponse(Guid id, string name, string imageName)
        {
            Id = id;
            Name = name;
            ImageName = imageName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }
    }
}
