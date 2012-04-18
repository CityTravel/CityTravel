namespace CityTravel.Domain.DomainModel
{
    /// <summary>
    /// Get interface for creation dbcontext.
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// Locker for creation database context factory;
        /// </summary>
        private static readonly object Locker = new object();
     
        /// <summary>
        /// DatabaseContext factory.
        /// </summary>
        private static volatile DbContextFactory contextFactory;
       
        /// <summary>
        /// Database context.
        /// </summary>
        private DataBaseContext context;
        
        /// <summary>
        /// Gets the get instance.
        /// </summary>
        public static DbContextFactory GetInstance
        {
            get
            {
                    lock (Locker)
                    {
                        if (contextFactory == null)
                        {
                            contextFactory = new DbContextFactory();
                        }
                    }

                return contextFactory;
            }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns></returns>
        public DataBaseContext GetContext()
        {
            lock (Locker)
            {
                return this.context ?? (this.context = new DataBaseContext());
            }
        }

    }
}
