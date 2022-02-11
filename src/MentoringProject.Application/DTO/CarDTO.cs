using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.DTO
{
    public record CarDTO
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Color { get; set; }

        public virtual ICollection<OwnerCarDTO> OwnerCarDTO { get; set; }
    }
}
