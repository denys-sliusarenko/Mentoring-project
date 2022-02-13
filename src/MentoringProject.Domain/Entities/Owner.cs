using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentoringProject.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<OwnerCar> OwnersCars { get; set; }
    }
}
