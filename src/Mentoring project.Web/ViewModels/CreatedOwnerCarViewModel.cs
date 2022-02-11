using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentoringProject.ViewModels
{
    public class CreatedOwnerCarViewModel
    {
        public Guid CarId { get; set; }

        public Guid OwnerId { get; set; }

        public string RegistrationNumber { get; set; }
    }
}