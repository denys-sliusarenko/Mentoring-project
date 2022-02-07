using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.DTO
{
    public class OwnerCarDTO
    {
        public Guid CarId { get; set; }

        public CarDTO Car { get; set; }

        public Guid OwnerId { get; set; }

        public OwnerDTO Owner { get; set; }

        public string RegistrationNumber { get; set; }
    }
}
