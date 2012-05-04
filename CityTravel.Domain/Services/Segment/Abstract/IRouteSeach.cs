namespace CityTravel.Domain.Services.Segment.Abstract
{
    using System.Collections.Generic;
    using CityTravel.Domain.Entities.Route;
    using Microsoft.SqlServer.Types;

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
        /// <param name="invalidDirections">The invalid directions.</param>
        /// <param name="validWords">The valid words.</param>
        /// <param name="invalidWords">The invalid words.</param>
        /// <param name="startMarker">The start marker.</param>
        /// <param name="endMarker">The end marker.</param>
        /// <param name="transportTypes">The transport types.</param>
        /// <returns>
        /// Appropriates routes
        /// </returns>
        List<Route> GetAppropriateRoutes(
            IEnumerable<Route> routes,
            IEnumerable<string> invalidDirections,
            IEnumerable<string> validWords,
            IEnumerable<string> invalidWords,
            SqlGeography startMarker,
            SqlGeography endMarker,
            IEnumerable<Transport> transportTypes);
    }
}
