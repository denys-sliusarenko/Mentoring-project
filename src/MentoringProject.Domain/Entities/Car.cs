using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Brand { get; set; }

        public string Color { get; set; }

        public virtual ICollection<OwnerCar> OwnerCars { get; set; }
    }
}
