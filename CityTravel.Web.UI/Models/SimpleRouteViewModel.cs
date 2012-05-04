using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityTravel.Web.UI.Models
{
    using CityTravel.Domain.Entities.Route;

    public class SimpleRouteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<MapPoint> Points { get; set; }
        
    }
}