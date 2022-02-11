using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.DTO
{
    public record OwnerCarDTO
    {
        public Guid Id { get; set; }

        public Guid CarId { get; set; }

        public virtual CarDTO Car { get; set; }

        public Guid OwnerId { get; set; }

        public virtual OwnerDTO Owner { get; set; }

        public string RegistrationNumber { get; set; }
    }
}
