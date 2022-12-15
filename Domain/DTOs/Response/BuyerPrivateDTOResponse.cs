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

        public BuyerPrivateDTOResponse(Guid id, string name, string? imageUrl, AddressDTOResponse? address,
            UserAuthDTOResponse userAuth)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Address = address;
            UserAuth = userAuth;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? ImageUrl { get; set; }

        public AddressDTOResponse? Address { get; set; }

        public UserAuthDTOResponse UserAuth { get; set; }
    }
}
