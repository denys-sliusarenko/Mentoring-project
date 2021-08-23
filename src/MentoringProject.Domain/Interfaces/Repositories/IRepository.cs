﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        Task<bool> Exist(int id);
    }
}
