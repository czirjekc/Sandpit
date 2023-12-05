using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Reflection;

namespace Eu.Europa.Ec.Olaf.GmsrDAL
{
    public class FakeRepository : IRepository
    {
        #region Fields

        private static List<IEntityWithKey> entityList = new List<IEntityWithKey>();

        #endregion

        #region Public Methods

        public static void Clear()
        {
            entityList.Clear();
        }

        public FakeRepository()
        {

        }

        public T Get<T>(int id) where T : class, IEntityWithKey
        {
            return (T)entityList.Find(x => (int)(x.EntityKey.EntityKeyValues[0].Value) == id);
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityWithKey
        {
            List<T> tList = new List<T>();
            entityList.ForEach(entity =>
            {
                if (entity.GetType() == typeof(T))
                {
                    tList.Add((T)entity);
                }
            });

            return tList.Where(predicate.Compile()).FirstOrDefault();
        }

        public IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityWithKey
        {
            List<T> tList = new List<T>();
            entityList.ForEach(entity =>
            {
                if (entity.GetType() == typeof(T))
                {
                    tList.Add((T)entity);
                }
            });

            return tList.Where(predicate.Compile()).AsQueryable();
        }

        public T Save<T>(T entity, string userLogin) where T : class, IEntityWithKey
        {
            if ((int)(entity.EntityKey.EntityKeyValues[0].Value) != 0)
            {
                entityList.RemoveAll(x => (int)(x.EntityKey.EntityKeyValues[0].Value) == (int)(entity.EntityKey.EntityKeyValues[0].Value));
                entityList.Add(entity);
            }
            else
            {
                EntityKeyMember entityKeyMember = new EntityKeyMember("Id", entityList.Count() + 1);

                // create a new entity with the same property values and a new id
                T newEntity = (T)Activator.CreateInstance(entity.GetType());
                newEntity.EntityKey = new EntityKey(entity.EntityKey.EntityContainerName + "." + entity.EntityKey.EntitySetName, "Id", entityList.Count() + 1);
                PropertyInfo[] entityProperties = newEntity.GetType().GetProperties();
                foreach (PropertyInfo entityProperty in entityProperties)
                {
                    if (entityProperty.PropertyType.IsPrimitive || entityProperty.PropertyType == typeof(String) || entityProperty.PropertyType == typeof(Nullable<int>) || (typeof(IEntityWithKey).IsAssignableFrom(entityProperty.PropertyType) && entityProperty.GetValue(entity, null) != null))
                    {
                        if (entityProperty.Name == "Id")
                            entityProperty.SetValue(newEntity, entityList.Count() + 1, null);
                        else
                            entityProperty.SetValue(newEntity, entityProperty.GetValue(entity, null), null);
                    }
                }


                PropertyInfo[] properties = newEntity.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Type propertyType = property.PropertyType;
                    // create instances for the string properties that are null
                    if (property.PropertyType == typeof(String) && property.GetValue(newEntity, null) == null)
                    {
                        property.SetValue(newEntity, string.Empty, null);
                    }
                    // create instances for the navigation properties (that are not collections) and create instances for the string properties
                    else if (typeof(IEntityWithKey).IsAssignableFrom(propertyType) && property.GetValue(newEntity, null) == null)
                    {
                        Object navigationEntity = Activator.CreateInstance(propertyType);
                        PropertyInfo[] navigationProperties = navigationEntity.GetType().GetProperties();
                        foreach (PropertyInfo navigationProperty in navigationProperties)
                        {
                            if (navigationProperty.PropertyType == typeof(String))
                            {
                                navigationProperty.SetValue(navigationEntity, string.Empty, null);
                            }
                        }

                        property.SetValue(newEntity, navigationEntity, null);
                    }
                }
                entityList.Add(newEntity);
            }

            return entity;
        }

        public T Delete<T>(T entity, string userLogin) where T : class, IEntityWithKey
        {
            entityList.RemoveAll(x => x.EntityKey == entity.EntityKey);

            return entity;
        }

        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
