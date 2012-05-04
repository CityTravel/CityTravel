namespace CityTravel.Domain.Repository.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using CityTravel.Domain.DomainModel.Abstract;
    using CityTravel.Domain.Entities;
    using CityTravel.Domain.Repository.Abstract;
    using CityTravel.Domain.Services.CacheProvider.Abstract;
    using CityTravel.Domain.Services.CacheProvider.Concrete;
    using CityTravel.Domain.Settings;

    /// <summary>
    /// The generic repository.
    /// </summary>
    /// <typeparam name="T">
    /// type of objects in repository
    /// </typeparam>
    public class GenericRepository<T> : IProvider<T>
        where T : BaseEntity
    {
        #region Constants and Fields

        /// <summary>
        /// The context.
        /// </summary>
        private readonly IDataBaseContext context;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public GenericRepository(IDataBaseContext context)
        {
            this.context = context;
            this.Cache = new DefaultCacheProvider();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.DbSet.Count();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Cache.
        /// </summary>
        protected ICacheProvider Cache { get; set; }

        /// <summary>
        ///   Gets the db set.
        /// </summary>
        private IDbSet<T> DbSet
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the specified t.
        /// </summary>
        /// <param name="entity">
        /// The entity. 
        /// </param>
        public void Add(T entity)
        {
            this.ClearCache();
            this.DbSet.Add(entity);
            this.Cache.Set(typeof(T).ToString(), this.DbSet.ToList(), TimeSpan.FromMinutes(GeneralSettings.CacheTime));
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>
        /// all objects
        /// </returns>
        public IEnumerable<T> All()
        {
            var data = this.Cache.Get(typeof(T).ToString()) as List<T>;

            if (data == null)
            {
                data = this.DbSet.ToList();
                if (data.Any())
                {
                    this.Cache.Set(typeof(T).ToString(), data, TimeSpan.FromMinutes(GeneralSettings.CacheTime));
                }
            }

            return this.DbSet.ToList();
        }

        /// <summary>
        /// Determines whether [contains] [the specified predicate].
        /// </summary>
        /// <param name="predicate">
        /// The predicate. 
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified predicate]; otherwise, <c>false</c> . 
        /// </returns>
        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Count(predicate) > 0;
        }

        /// <summary>
        /// Deletes the specified t.
        /// </summary>
        /// <param name="entity">
        /// The entity. 
        /// </param>
        public void Delete(T entity)
        {
            this.ClearCache();
            this.DbSet.Remove(entity);
            this.Cache.Set(typeof(T).ToString(), this.DbSet.ToList(), TimeSpan.FromMinutes(GeneralSettings.CacheTime));
        }

        /// <summary>
        /// Deletes the specified predicate.
        /// </summary>
        /// <param name="predicate">
        /// The predicate. 
        /// </param>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            this.ClearCache();
            var objects = this.Filter(predicate).ToList();
            var count = objects.Count();
            for (int i = 0; i < count; i++)
            {
                this.DbSet.Remove(objects[i]);
            }

            this.Cache.Set(typeof(T).ToString(), this.DbSet.ToList(), TimeSpan.FromMinutes(GeneralSettings.CacheTime));
        }

        /// <summary>
        /// Filters the specified predicate.
        /// </summary>
        /// <param name="predicate">
        /// The predicate. 
        /// </param>
        /// <returns>
        /// filters objects
        /// </returns>
        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="predicate">
        /// The predicate. 
        /// </param>
        /// <returns>
        /// find object by predicate
        /// </returns>
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">
        /// The id. 
        /// </param>
        /// <returns>
        /// object found
        /// </returns>
        public T GetById(int id)
        {
            return this.Find(x => x.Id == id);
        }

        /// <summary>
        /// Updates the specified t.
        /// </summary>
        /// <param name="entity">
        /// The entity. 
        /// </param>
        public void Update(T entity)
        {
            var data = this.Cache.Get(typeof(T).ToString()) as List<T>;
            if (data != null)
            {
                // TO-DO:data.invvalidate(typeOf(T).toString());
                var index = data.FindIndex(type => type.Id == entity.Id);
                data[index] = entity;
                this.Cache.Set(typeof(T).ToString(), data, TimeSpan.FromMinutes(GeneralSettings.CacheTime));
                this.DbSet.Attach(entity);
            }
            else
            {
                this.DbSet.Attach(entity);
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Save result.</returns>
        public int Save()
        {
            return this.context.SaveChanges();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears the cache.
        /// </summary>
        protected void ClearCache()
        {
            this.Cache.Invalidate(typeof(T).ToString());
        }

        #endregion
    }
}