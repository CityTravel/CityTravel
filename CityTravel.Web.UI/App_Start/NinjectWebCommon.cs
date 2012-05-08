[assembly: WebActivator.PreApplicationStartMethod(typeof(CityTravel.Web.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(CityTravel.Web.UI.App_Start.NinjectWebCommon), "Stop")]

namespace CityTravel.Web.UI.App_Start
{
    using System;
    using System.Web;
    using CityTravel.Domain.DomainModel.Abstract;
    using CityTravel.Domain.DomainModel.Concrete;
    using CityTravel.Domain.Repository.Abstract;
    using CityTravel.Domain.Repository.Concrete;
    using CityTravel.Domain.Services.AuthenticationProvider.Abstract;
    using CityTravel.Domain.Services.AuthenticationProvider.Concrete;
    using CityTravel.Domain.Services.Autocomplete.Abstract;
    using CityTravel.Domain.Services.Autocomplete.Concrete;
    using CityTravel.Domain.Services.Segment.Abstract;
    using CityTravel.Domain.Services.Segment.Concrete;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IProvider<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IAutocomplete>().To<CacheAutoComplete>().InThreadScope();
            kernel.Bind<IRouteSeach>().To<RouteSeach>();
            kernel.Bind<IDataBaseContext>().To<DataBaseContext>().InThreadScope();
            kernel.Bind<IAuthenticationProvider>().To<FormsAuthenticationProvider>().InThreadScope();

        }
    }
}
