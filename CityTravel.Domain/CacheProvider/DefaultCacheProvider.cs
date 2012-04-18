using System;
using System.Web;
using System.Web.Caching;
using CityTravel.Domain.Abstract;

namespace CityTravel.Domain.CacheProvider
{
    /// <summary>
    /// Default Cache Provider
    /// </summary>
    public class DefaultCacheProvider : ICacheProvider
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCacheProvider"/> class.
        /// </summary>
        public DefaultCacheProvider()
        {
            this.Cache = HttpRuntime.Cache;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the cache.
        /// </summary>
        public Cache Cache { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">
        /// The key. 
        /// </param>
        /// <returns>
        /// The get.
        /// </returns>
        public object Get(string key)
        {
            return this.Cache.Get(key);
        }

        /// <summary>
        /// Invalidates the specified key.
        /// </summary>
        /// <param name="key">
        /// The key. 
        /// </param>
        public void Invalidate(string key)
        {
            this.Cache.Remove(key);
        }

        /// <summary>
        /// Determines whether the specified key is set.
        /// </summary>
        /// <param name="key">
        /// The key. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified key is set; otherwise, <c>false</c> . 
        /// </returns>
        public bool IsSet(string key)
        {
            return this.Cache[key] != null;
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">
        /// The key. 
        /// </param>
        /// <param name="data">
        /// The data. 
        /// </param>
        /// <param name="cacheTime">
        /// The cache time. 
        /// </param>
        public void Set(string key, object data, TimeSpan cacheTime)
        {
            this.Cache.Insert(key, data, null, Cache.NoAbsoluteExpiration, cacheTime, CacheItemPriority.Normal, null);
        }

        #endregion
    }
}