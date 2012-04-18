using System.Collections.Generic;
using CityTravel.Domain.Entities;
using Microsoft.SqlServer.Types;

namespace CityTravel.Domain.Abstract
{
    /// <summary>
    /// Interface of RouteSeach for algorithm behavior
    /// </summary>
    public interface IRouteSeach
    {
        /// <summary>
        /// Gets or sets the appropriate routes.
        /// </summary>
        /// <value>
        /// The appropriate routes.
        /// </value>
        List<Route> AppropriateRoutes { get; set; }

        /// <summary>
        /// Gets the appropriate routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <param name="transportTypes">The transport types.</param>
        /// <returns>
        /// Appropriates routes
        /// </returns>
        List<Route> GetAppropriateRoutes(
             IEnumerable<Route> routes,
            SqlGeography startMarker,
            SqlGeography endMarker,
            IEnumerable<Transport> transportTypes);
    }
}
