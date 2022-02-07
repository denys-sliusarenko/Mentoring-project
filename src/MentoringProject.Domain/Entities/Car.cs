using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Owner> Owners { get; set; }//not use yet
    }
}
