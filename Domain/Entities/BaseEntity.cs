using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity(Guid id)
        {
            Id = id;
        }

        public BaseEntity()
        {

        }

        public virtual Guid Id { get; protected set; }

        public void GenerateId()
        {
            Id = Guid.NewGuid();
        }
    }
}
