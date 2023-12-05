using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Eu.Europa.Ec.Olaf.GmsrDAL
{
    public interface IRepository : IDisposable
    {
        T Get<T>(int id) where T : class, IEntityWithKey;
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityWithKey;
        
        IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityWithKey;

        T Save<T>(T entity, string userLogin) where T : class, IEntityWithKey;
        T Delete<T>(T entity, string userLogin) where T : class, IEntityWithKey;
    }
}
