using System.Text;

namespace savings_app_backend.Models.Entities
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public UserAuth UserAuth { get; set; }
        public Guid UserAuthId { get; set; }
        //public Guid UserAuthId { get; set; }
        public string Name { get; set; }
        public string? Picture { get; set; }

    }
}
