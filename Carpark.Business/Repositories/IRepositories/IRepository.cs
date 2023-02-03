﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carpark.Business.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null);
        public Task<T> GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null);
    }
}
