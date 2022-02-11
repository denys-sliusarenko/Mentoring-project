using MentoringProject.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentoringProject.ViewModels
{
    public class OwnerCarViewModel
    {
        public Guid Id { get; set; }

        public CarViewModel Car { get; set; }

        public string RegistrationNumber { get; set; }
    }
}