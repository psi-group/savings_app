using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Response
{
    public class BuyerPrivateDTOResponse
    {

        public BuyerPrivateDTOResponse(Guid id, string name, string imageName, AddressDTOResponse? address,
            UserAuthDTOResponse userAuth)
        {
            Id = id;
            Name = name;
            ImageName = imageName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public Address? Address { get; set; }

        public UserAuth UserAuth { get; set; }
    }
}
