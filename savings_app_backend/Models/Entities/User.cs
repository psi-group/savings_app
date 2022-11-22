using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace savings_app_backend.Models.Entities
{
    public abstract class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Order>? Orders { get; set; }

        public UserAuth? UserAuth { get; set; }
        public Guid UserAuthId { get; set; }

        public Address? Address { get; set; }
        public Guid AddressId { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
