﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.Domain.Repository
{
    public interface IGenericRepository<T, E> where E : class where T : class
    {
        T GetById(Guid id);
        IReadOnlyCollection<T> GetAll();
        void Delete(T obj);
        void Create(T obj);
        void Update(T obj);
        void Save();
    }
}
