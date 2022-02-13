using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Entities
{
    public class OwnerCar
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CarId { get; set; }

        public virtual Car Car { get; set; }

        public Guid OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public string RegistrationNumber { get; set; }
    }
}
