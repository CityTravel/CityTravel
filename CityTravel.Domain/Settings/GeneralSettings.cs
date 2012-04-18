using System.Configuration;

namespace CityTravel.Domain.Settings
{
    /// <summary>
    /// Settigs for City Travel project
    /// </summary>
    public class GeneralSettings
    {
        #region Constants and Fields

        /// <summary>
        ///   Default cache value
        /// </summary>
        private const int DefaultCache = 5;    

        /// <summary>
        /// Default radius seach for stops
        /// </summary>
        private const int DefaultStopRadiusSeach = 500;
       
        /// <summary>
        /// Default route radius seach
        /// </summary>
        private const int DefaultRouteRadiusSeach = 1500;

        /// <summary>
        /// Default time constraint
        /// </summary>
        private const int DefaultTimeConstraint = 2;

        /// <summary>
        /// Default walking speed
        /// </summary>
        private const int DefaultWalkingSpeed = 5;

        /// <summary>
        /// Max find index deflection
        /// </summary>
        private const int DefaultFindIndexDeflection = 30;

        /// <summary>
        /// Index deflection
        /// </summary>
        private const int DefaultIndexDeflection = 7;

        /// <summary>
        /// Max relations buffer
        /// </summary>
        private const int DefaultMaxRelationsBuffer = 1;

		/// <summary>
		/// Coordinates of the boundary of Dnepropetrovsk SouthWest Longitude
		/// </summary>
		private static double southWestLongitude = 48.38544219115483;

		/// <summary>
		/// Coordinates of the boundary of Dnepropetrovsk SouthWest Latitude
		/// </summary>
		private static double southWestLatitude = 34.8953247070312;

		/// <summary>
		/// Coordinates of the boundary of Dnepropetrovsk NorthEast Longitude
		/// </summary>
		private static double northEastLongitude = 48.510236244324055;

		/// <summary>
		/// Coordinates of the boundary of Dnepropetrovsk NorthEast Latitude
		/// </summary>
		private static double northEastLatitude = 35.13153076171875;

        /// <summary>
        ///   Google api key
        /// </summary>
        private const string DefaultGoogleApiKey = "AIzaSyAESAHKfk6MOaE16uNuHYvxZp47wei6uMo";


        /// <summary>
        /// Spatial Reference System for SQL
        /// </summary>
        private const int DefaultSpatialReferenceSystem = 4326;

        /// <summary>
		/// Default Distance Token String
        /// </summary>
        private const string DefaultDistanceTokenString = "routes[0].legs[0].distance.value";

        /// <summary>
		/// Default Polyline Points Token
        /// </summary>
        private const string DefaultPolylinePointsToken = "routes[0].overview_polyline.points";

        /// <summary>
		/// Default Time Direction Token
        /// </summary>
        private const string DefaultTimeDirectionToken = "routes[0].legs[0].duration.value";

        /// <summary>
		/// Default Distance Matrix Token
        /// </summary>
        private const string DefaultDistanceMatrixToken = "rows[0].elements[0].distance.value";

        /// <summary>
		/// Default City Name for Service
        /// </summary>
        private const string DefaultCityNameForService = "Днепропетровск Днепропетровская область ";
  
        /// <summary>
		/// Default Google Maps Api Key
        /// </summary>
        private const string DefaultGoogleMapsApiKey = "AIzaSyAESAHKfk6MOaE16uNuHYvxZp47wei6uMo";

        /// <summary>
        /// Default Header of the Google Maps Api for the Autocomplete
        /// </summary>
        private const string DefaultJsonReqAutocompleteHead = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";

		/// <summary>
		/// Body of the Google Maps Api for the Autocomplete
		/// </summary>
		private const string DefaultJsonReqAutocompleteBody = "&types=geocode&language=ru&sensor=false&key=";


        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the cache time.
        /// </summary>
        public static int CacheTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheTime"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["CacheTime"])
                           : DefaultCache;
            }
        }

        /// <summary>
        ///   Gets the google API key.
        /// </summary>
        public static string GoogleApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["GoogleApiKey"] ?? DefaultGoogleApiKey;
            }
        }

        /// <summary>
        /// Gets the route radius seach.
        /// </summary>
        public static int RouteRadiusSeach
        {
            get
            {
                return ConfigurationManager.AppSettings["RouteRadiusSeach"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["RouteRadiusSeach"])
                           : DefaultRouteRadiusSeach;
            }
        }

        /// <summary>
        /// Gets the max time constraint.
        /// </summary>
        public static int MaxTimeConstraint 
        {
            get
            {
                return ConfigurationManager.AppSettings["MaxTimeConstraint"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["MaxTimeConstraint"])
                           : DefaultTimeConstraint;
            }
        }

        /// <summary>
        /// Gets WalkingSpeed.
        /// </summary>
        public static int WalkingSpeed
        {
            get
            {
                return ConfigurationManager.AppSettings["WalkingSpeed"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["WalkingSpeed"])
                           : DefaultWalkingSpeed;
            }
        }

        /// <summary>
        /// Gets the max stop radius seach.
        /// </summary>
        public static int MaxStopRadiusSeach
        {
            get
            {
                return ConfigurationManager.AppSettings["MaxStopRadiusSeach"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["MaxStopRadiusSeach"])
                           : DefaultStopRadiusSeach;
            }
        }

        /// <summary>
        /// Gets MaxFindIndexDeflection.
        /// </summary>
        public static int MaxFindIndexDeflection
        {
            get
            {
                return ConfigurationManager.AppSettings["MaxFindIndexDeflection"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["MaxFindIndexDeflection"])
                           : DefaultFindIndexDeflection;
            }
        }

        /// <summary>
        /// Gets the max index deflection.
        /// </summary>
        public static int MaxIndexDeflection
        {
            get        
            {
                return ConfigurationManager.AppSettings["MaxIndexDeflection"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["MaxIndexDeflection"])
                           : DefaultIndexDeflection;
            }
        }

        /// <summary>
        /// Gets the mar buffer for refresh relations.
        /// </summary>
        public static int MaxRelationsBuffer
        {
           get
           {
               return ConfigurationManager.AppSettings["MaxRelationsBuffer"] != null
                           ? int.Parse(ConfigurationManager.AppSettings["MaxRelationsBuffer"])
                           : DefaultMaxRelationsBuffer;
           }
        }

		/// <summary>
		/// Gets the Coordinates of the City. Get SouthWest Longitude
		/// </summary>
		public static double GetSouthWestLongitude
		{
			get
			{
				return ConfigurationManager.AppSettings["GetSouthWestLongitude"] != null ? double.Parse(ConfigurationManager.AppSettings["GetSouthWestLongitude"]) : southWestLongitude;
			}
		}

		/// <summary>
		/// Gets the Coordinates of the City. Get SouthWest Latitude
		/// </summary>
		public static double GetSouthWestLatitude
		{
			get
			{
				return ConfigurationManager.AppSettings["GetSouthWestLatitude"] != null ? double.Parse(ConfigurationManager.AppSettings["GetSouthWestLatitude"]) : southWestLatitude;
			}
		}

		/// <summary>
		/// Gets the Coordinates of the City. Get NorthEast Longitude
		/// </summary>
		public static double GetNorthEastLongitude
		{
			get
			{
				return ConfigurationManager.AppSettings["GetNorthEastLongitude"] != null ? double.Parse(ConfigurationManager.AppSettings["GetNorthEastLongitude"]) : northEastLongitude;
			}
		}

		/// <summary>
		/// Gets the Coordinates of the City. Get NorthEast Latitude
		/// </summary>
		public static double GetNorthEastLatitude
		{
			get
			{
				return ConfigurationManager.AppSettings["GetNorthEastLatitude"] != null ? double.Parse(ConfigurationManager.AppSettings["GetNorthEastLatitude"]) : northEastLatitude;
			}
		}

		/// <summary>
		/// Gets the Spatial Reference System for SQL
		/// </summary>
		public static int GetSpatialReferenceSystem
		{
			get
			{
				return ConfigurationManager.AppSettings["SpatialReferenceSystem"] != null ? int.Parse(ConfigurationManager.AppSettings["SpatialReferenceSystem"]) : DefaultSpatialReferenceSystem;
			}
		}

        /// <summary>
		/// Gets the Distance Token String for Google Maps Request
		/// </summary>
		public static string GetDistanceTokenString
		{
			get
			{
                return ConfigurationManager.AppSettings["DistanceTokenString"] != null ? ConfigurationManager.AppSettings["DistanceTokenString"] : DefaultDistanceTokenString;
			}
		}

		/// <summary>
		/// Gets the Polyline Points Token String for Google Maps Request
		/// </summary>
		public static string GetPolylinePointsToken
		{
			get
			{
				return ConfigurationManager.AppSettings["PolylinePointsToken"] != null ? ConfigurationManager.AppSettings["PolylinePointsToken"] : DefaultPolylinePointsToken;
			}
		}

		/// <summary>
		/// Gets the Time Direction Token String for Google Maps Request
		/// </summary>
		public static string GetTimeDirectionToken
		{
			get
			{
				return ConfigurationManager.AppSettings["TimeDirectionToken"] != null ? ConfigurationManager.AppSettings["TimeDirectionToken"] : DefaultTimeDirectionToken;
			}
		}

        /// <summary>
		/// Gets the Distance Matrix Token String for Google Maps Request
		/// </summary>
		public static string GetDistanceMatrixToken
		{
			get
			{
				return ConfigurationManager.AppSettings["DistanceMatrixToken"] != null ? ConfigurationManager.AppSettings["DistanceMatrixToken"] : DefaultDistanceMatrixToken;
			}
		}

        /// <summary>
		/// Gets the City Name For Service
		/// </summary>
		public static string GetCityNameForService
		{
			get
			{
				return ConfigurationManager.AppSettings["CityNameForService"] != null ? ConfigurationManager.AppSettings["CityNameForService"] : DefaultCityNameForService;
			}
		}

        /// <summary>
		/// Gets Google Maps Api Key
		/// </summary>
		public static string GetGoogleMapsApiKey
		{
			get
			{
				return ConfigurationManager.AppSettings["GoogleMapsApiKey"] != null ? ConfigurationManager.AppSettings["GoogleMapsApiKey"] : DefaultGoogleMapsApiKey;
			}
		}

        /// <summary>
		/// Gets Json Request Autocomplete Head for Google Maps Api
		/// </summary>
		public static string GetJsonReqAutocompleteHead
		{
			get
			{
				return ConfigurationManager.AppSettings["JsonReqAutocompleteHead"] != null ? ConfigurationManager.AppSettings["JsonReqAutocompleteHead"] : DefaultJsonReqAutocompleteHead;
			}
		}

		/// <summary>
		/// Gets Json Request Autocomplete Body for Google Maps Api
		/// </summary>
		public static string GetJsonReqAutocompleteBody
		{
			get
			{
				return ConfigurationManager.AppSettings["JsonReqAutocompleteBody"] != null ? ConfigurationManager.AppSettings["JsonReqAutocompleteBody"] : DefaultJsonReqAutocompleteBody;
			}
		}

		/// <summary>
		/// Gets Google Map Distance Value
		/// </summary>
		public static string GetGoogleMapDistanceValueString
		{
			get
			{
				return ConfigurationManager.AppSettings["GoogleMapDistanceValueString"];
			}
		}

		/// <summary>
		/// Gets Google Map Html Instruction
		/// </summary>
		public static string GetGoogleMapHtmlInstruction
		{
			get
			{
				return ConfigurationManager.AppSettings["GoogleMapHtmlInstruction"];
			}
		}
			

        #endregion
    }
}