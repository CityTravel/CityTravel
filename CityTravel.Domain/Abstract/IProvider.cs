using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CityTravel.Domain.Entities;

namespace CityTravel.Domain.Abstract
{
    using System.Linq;

    /// <summary>
    /// Basic provider interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of objects in repository 
    /// </typeparam>
    public interface IProvider<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Gets the total objects count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets all objects from database.
        /// </summary>
        /// <returns>
        /// The all.
        /// </returns>
        IEnumerable<T> All();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">
        /// Specified filter. 
        /// </param>
        /// <returns>
        /// The filter.
        /// </returns>
        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">
        /// Specified the filter expression. 
        /// </param>
        /// <returns>
        /// The contains.
        /// </returns>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">
        /// Find condition
        /// </param>
        /// <returns>
        /// The find.
        /// </returns>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find entity by specified id.
        /// </summary>
        /// <param name="id">
        /// entity id 
        /// </param>
        /// <returns>
        /// The get by id.
        /// </returns>
        T GetById(int id);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">
        /// Specified a new object to create. 
        /// </param>
        void Add(T t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">
        /// Specified a existing object to delete. 
        /// </param>
        void Delete(T t);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate">
        /// delete condition
        /// </param>
        void Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">
        /// Specified the object to save. 
        /// </param>
        void Update(T t);

        int Save();
    }
}