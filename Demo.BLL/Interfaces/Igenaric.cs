﻿using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface Igenaric<T> where T : Base
    {
       Task<IEnumerable<T>> GetAll();
       Task<T> Get(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
