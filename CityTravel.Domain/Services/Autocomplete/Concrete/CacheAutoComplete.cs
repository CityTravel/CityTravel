namespace CityTravel.Domain.Services.Autocomplete.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;

    using CityTravel.Domain.Entities.Autocomplete;
    using CityTravel.Domain.Repository;
    using CityTravel.Domain.Repository.Abstract;

    public class CacheAutoComplete : Autocomplete
    {
        private readonly System.Web.Caching.Cache cache;

        public CacheAutoComplete(
            IProvider<Place> placeRepository,
            IProvider<Building> buildingRepository,
            System.Web.Caching.Cache cacheRepository)
            : base(placeRepository, buildingRepository)
        {
            this.cache = HttpRuntime.Cache;
            this.CacheTimeOut = 3000;
        }

        public CacheAutoComplete(IProvider<Place> placeRepository, IProvider<Building> buildingRepository)
            : this(placeRepository, buildingRepository, System.Web.HttpContext.Current.Cache)
        {
        }

        /// <summary>
        /// Cache timeout in milliseconds
        /// </summary>
        public int CacheTimeOut { set; get; }

        public override object GetAdressFromDatabase(string inputAdress)
        {
            if (this.cache[inputAdress] != null)
            {
                return this.cache[inputAdress];
            }

            return base.GetAdressFromDatabase(inputAdress);
        }

        public override void AddSuggestionsToDatabase(List<string> suggestions, string inputAdress = null)
        {
            if (!string.IsNullOrEmpty(inputAdress) && !string.IsNullOrWhiteSpace(inputAdress))
            {
                this.cache.Add(inputAdress, suggestions, null, DateTime.Now.AddMilliseconds(this.CacheTimeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);

            }

            Task.Factory.StartNew(() =>
            {
                base.AddSuggestionsToDatabase(suggestions);
            });
        }

    }
}
