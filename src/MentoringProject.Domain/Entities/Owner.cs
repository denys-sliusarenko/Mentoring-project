using System;
using System.Collections.Generic;

namespace MentoringProject.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
