﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSharp.DAL.EFCoreRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public void Create(T obj);
        public void Delete(Guid id);
        public IReadOnlyCollection<T> GetAll();
        public T GetById(Guid id);
        public void Update(T obj);
        public void Save();
        public void Detach(T obj);
    }
}
