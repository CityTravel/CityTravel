using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CityTravel.Domain.Entities;

namespace CityTravel.Tests.Domain.DomainModel
{
    /// <summary>
    /// The fake db set.
    /// </summary>
    /// <typeparam name="T">
    /// type of objects in set
    /// </typeparam>
    public class FakeDbSet<T> : IDbSet<T>
        where T : BaseEntity, new()
    {
        #region Constants and Fields

        /// <summary>
        /// The data.
        /// </summary>
        private readonly HashSet<T> data;

        /// <summary>
        /// The query.
        /// </summary>
        private readonly IQueryable query;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbSet{T}"/> class.
        /// </summary>
        public FakeDbSet()
        {
            this.data = new HashSet<T>();
            this.query = this.data.AsQueryable();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets ElementType.
        /// </summary>
        public Type ElementType
        {
            get
            {
                return this.query.ElementType;
            }
        }

        /// <summary>
        /// Gets Expression.
        /// </summary>
        public Expression Expression
        {
            get
            {
                return this.query.Expression;
            }
        }

        /// <summary>
        /// Gets Local.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// throws exception
        /// </exception>
        public ObservableCollection<T> Local
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets Provider.
        /// </summary>
        public IQueryProvider Provider
        {
            get
            {
                return this.query.Provider;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// added entity
        /// </returns>
        public T Add(T entity)
        {
            this.data.Add(entity);
            return entity;
        }

        /// <summary>
        /// The attach.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// added entity
        /// </returns>
        public T Attach(T entity)
        {
            this.data.Add(entity);
            return entity;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// created object
        /// </returns>
        public T Create()
        {
            return new T();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <typeparam name="TDerivedEntity">
        /// derived entity type
        /// </typeparam>
        /// <returns>
        /// new derived entity
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// throws exception
        /// </exception>
        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// found object
        /// </returns>
        public T Find(params object[] keyValues)
        {
            return this.data.SingleOrDefault(x => x.Id == (int)keyValues[0]);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// data enumerator
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// removed entity
        /// </returns>
        public T Remove(T entity)
        {
            this.data.Remove(entity);
            return entity;
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// data enumerator
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        #endregion
    }
}