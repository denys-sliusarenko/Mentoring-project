﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentoringProject.Application.DTO
{
    public class OwnerDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<OwnerCarDTO> OwnersCars { get; set; }
    }
}