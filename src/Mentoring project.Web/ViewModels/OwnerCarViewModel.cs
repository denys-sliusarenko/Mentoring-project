using System;

namespace MentoringProject.ViewModels
{
    public class OwnerCarViewModel
    {
        public Guid Id { get; set; }

        public CarViewModel Car { get; set; }

        public string RegistrationNumber { get; set; }
    }
}