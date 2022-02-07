using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Entities
{
    public class OwnerCar
    {
        public Guid CarId { get; set; }

        public Car Car { get; set; }

        public Guid OwnerId { get; set; }

        public Owner Owner { get; set; }

        public string RegistrationNumber { get; set; }
    }
}
