using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Eu.Europa.Ec.Olaf.GmsrDAL
{
    public class EntityFrameworkRepository : IRepository
    {
        #region Fields

        private readonly ObjectContext context;

        #endregion

        #region Public Methods

        public EntityFrameworkRepository()
        {
            context = new GmsrEntities();
        }

        public EntityFrameworkRepository(ObjectContext objectContext)
        {
            context = objectContext;
        }

        public T Get<T>(int id) where T : class, IEntityWithKey
        {
            IEnumerable<KeyValuePair<string, object>> entityKeyValues =
                new[] { new KeyValuePair<string, object>("Id", id) };

            var key = new EntityKey(context.DefaultContainerName + "." + context.GetEntitySet<T>().Name, entityKeyValues);

            return (T)context.GetObjectByKey(key);
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityWithKey
        {
            return context.CreateQuery<T>("[" + context.GetEntitySet<T>().Name + "]")
                .Where(predicate)
                .FirstOrDefault();
        }

        public IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityWithKey
        {
            return context
                .CreateQuery<T>("[" + context.GetEntitySet<T>().Name + "]")
                .Where(predicate);
        }

        public T Save<T>(T entity, string userLogin) where T : class, IEntityWithKey
        {
            object originalItem;

            if (context.GetType() == typeof(GmsrEntities)) // only audittrailing for GMS database
                ((GmsrEntities)context).UserLogin = userLogin;            
            
            if (context.TryGetObjectByKey(entity.EntityKey, out originalItem))
            {                
                context.ApplyCurrentValues<T>(entity.EntityKey.EntitySetName, entity);
            }
            else
            {
                context.AddObject(context.GetEntitySet<T>().Name, entity);
            }

            context.SaveChanges();

            return entity;
        }

        public T Delete<T>(T entity, string userLogin) where T : class, IEntityWithKey
        {
            if (context.GetType() == typeof(GmsrEntities)) // only audittrailing for GMS database
                ((GmsrEntities)context).UserLogin = userLogin;

            context.Attach(entity);
            context.DeleteObject(entity);
            context.SaveChanges();

            return entity;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        #endregion
    }
}
