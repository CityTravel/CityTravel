using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using CityTravel.Domain.DomainModel;
using CityTravel.Domain.Entities;
using Microsoft.SqlServer.Types;

namespace CityTravel.Tests.Domain.DomainModel
{
    /// <summary>
    /// The fake db context.
    /// </summary>
    public class FakeDbContext : IDataBaseContext
    {
        #region Constants and Fields

        /// <summary>
        /// The stops.
        /// </summary>
        private readonly List<Stop> stops = new List<Stop>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="FakeDbContext" /> class.
        /// </summary>
        public FakeDbContext()
        {
            this.Routes = new FakeDbSet<Route>();
            this.Stops = new FakeDbSet<Stop>();
            this.Feedbacks = new FakeDbSet<Feedback>();
            this.TransportTypes = new FakeDbSet<TransportType>();
            this.Languages = new FakeDbSet<Language>();
            this.Buildings = new FakeDbSet<Building>();
            this.Places = new FakeDbSet<Place>();
            this.Init();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets Buildings.
        /// </summary>
        public IDbSet<Building> Buildings { get; private set; }

        /// <summary>
        ///   Gets Feedbacks.
        /// </summary>
        public IDbSet<Feedback> Feedbacks { get; private set; }

        /// <summary>
        ///   Gets Languages.
        /// </summary>
        public IDbSet<Language> Languages { get; private set; }

        /// <summary>
        ///   Gets Places.
        /// </summary>
        public IDbSet<Place> Places { get; private set; }

        /// <summary>
        ///   Gets Routes.
        /// </summary>
        public IDbSet<Route> Routes { get; private set; }

        /// <summary>
        ///   Gets Stops.
        /// </summary>
        public IDbSet<Stop> Stops { get; private set; }

        /// <summary>
        ///   Gets TransportTypes.
        /// </summary>
        public IDbSet<TransportType> TransportTypes { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// 0 rows were affected 
        /// </returns>
        public int SaveChanges()
        {
            return 0;
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <typeparam name="T">
        /// entity type 
        /// </typeparam>
        /// <returns>
        /// IDbSet of entities 
        /// </returns>
        public IDbSet<T> Set<T>() where T : BaseEntity
        {
            var properties = this.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyGenericType = propertyInfo.PropertyType.GetGenericArguments()[0];
                if (propertyGenericType == typeof(T))
                {
                    return this.GetType().GetProperty(propertyInfo.Name).GetValue(this, null) as IDbSet<T>;
                }
            }

            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The init.
        /// </summary>
        private void Init()
        {
            // fake stops for DbSet
            this.Stops.Add(
                new Stop
                    {
                        Name = "artema", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.0486559116951 48.462817307127736, 35.048655911702049 48.4623106340045, 35.04941999864343 48.4623106340056, 35.049419998636175 48.462817307136071, 35.0486559116951 48.462817307127736))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "cum", 
                        Id = 1, 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.045169590705079 48.464744895446, 35.045169590705747 48.464281656841862, 35.0458682031656 48.464281656842537, 35.045868203169746 48.464744895443019, 35.045169590705079 48.464744895446))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });

            this.Stops.Add(
                new Stop
                    {
                        Name = "barikadnaya", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.050442223614944 48.461860443262829, 35.050442223606083 48.461417739476694, 35.051109829606617 48.461417739483117, 35.051109829611406 48.461860443252853, 35.050442223614944 48.461860443262829))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "derzinskogo", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.053835863796607 48.460065353453423, 35.053835863809354 48.459598588695471, 35.054539729157995 48.459598588700537, 35.054539729158591 48.460065353456415, 35.053835863796607 48.460065353453423))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "klari", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.057115401244225 48.458320966310076, 35.057115401239876 48.457998977228812, 35.0576009338223 48.457998977233224, 35.057600933821959 48.458320966314538, 35.057115401244225 48.458320966310076))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "gonchara", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.061098928708667 48.456273291340779, 35.061098928710017 48.456111976653375, 35.061342168322909 48.45611197665076, 35.06134216832568 48.456273291341077, 35.061098928708667 48.456273291340779))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "shevchenko", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.0597494558555 48.454962498832295, 35.059749455858778 48.454854119312877, 35.059912872633241 48.454854119315058, 35.059912872633895 48.454962498833225, 35.0597494558555 48.454962498832295))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "patorz", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.057951014673854 48.453440671157757, 35.057951014675481 48.453380292655829, 35.058042051883817 48.4533802926538, 35.058042051887305 48.45344067115856, 35.057951014673854 48.453440671157757))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "chernishevskogo", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.055661926976008 48.451874192623052, 35.055661926973343 48.4516094655135, 35.0560610628346 48.451609465510053, 35.056061062831468 48.451874192618007, 35.055661926976008 48.451874192623052))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "gagarina", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.058935959663451 48.449766693295835, 35.058935959664261 48.449504260346906, 35.059331620143688 48.449504260343431, 35.059331620136234 48.44976669329327, 35.058935959663451 48.449766693295835))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "televezionnaya", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.05200743312669 48.443792557693918, 35.052007433127628 48.443727086838358, 35.052106129702281 48.443727086841122, 35.052106129705528 48.443792557695382, 35.05200743312669 48.443792557693918))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "kuri", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.050450858264746 48.440844838909406, 35.050450858262167 48.440790634115992, 35.050532566646758 48.440790634116794, 35.05053256664872 48.440844838911367, 35.050450858264746 48.440844838909406))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "lazariana", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.048426411509944 48.438419103271258, 35.048426411510405 48.438347925472577, 35.0485336998809 48.43834792547451, 35.048533699879179 48.438419103270796, 35.048426411509944 48.438419103271258))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });

            // fake stops for route
            this.stops.Add(
                new Stop
                    {
                        Name = "artema", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.0486559116951 48.462817307127736, 35.048655911702049 48.4623106340045, 35.04941999864343 48.4623106340056, 35.049419998636175 48.462817307136071, 35.0486559116951 48.462817307127736))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "cum", 
                        Id = 1, 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.045169590705079 48.464744895446, 35.045169590705747 48.464281656841862, 35.0458682031656 48.464281656842537, 35.045868203169746 48.464744895443019, 35.045169590705079 48.464744895446))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });

            this.stops.Add(
                new Stop
                    {
                        Name = "barikadnaya", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.050442223614944 48.461860443262829, 35.050442223606083 48.461417739476694, 35.051109829606617 48.461417739483117, 35.051109829611406 48.461860443252853, 35.050442223614944 48.461860443262829))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "derzinskogo", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.053835863796607 48.460065353453423, 35.053835863809354 48.459598588695471, 35.054539729157995 48.459598588700537, 35.054539729158591 48.460065353456415, 35.053835863796607 48.460065353453423))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "klari", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.057115401244225 48.458320966310076, 35.057115401239876 48.457998977228812, 35.0576009338223 48.457998977233224, 35.057600933821959 48.458320966314538, 35.057115401244225 48.458320966310076))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "gonchara", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.061098928708667 48.456273291340779, 35.061098928710017 48.456111976653375, 35.061342168322909 48.45611197665076, 35.06134216832568 48.456273291341077, 35.061098928708667 48.456273291340779))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "shevchenko", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.0597494558555 48.454962498832295, 35.059749455858778 48.454854119312877, 35.059912872633241 48.454854119315058, 35.059912872633895 48.454962498833225, 35.0597494558555 48.454962498832295))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.Stops.Add(
                new Stop
                    {
                        Name = "patorz", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.057951014673854 48.453440671157757, 35.057951014675481 48.453380292655829, 35.058042051883817 48.4533802926538, 35.058042051887305 48.45344067115856, 35.057951014673854 48.453440671157757))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "chernishevskogo", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.055661926976008 48.451874192623052, 35.055661926973343 48.4516094655135, 35.0560610628346 48.451609465510053, 35.056061062831468 48.451874192618007, 35.055661926976008 48.451874192623052))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "gagarina", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.058935959663451 48.449766693295835, 35.058935959664261 48.449504260346906, 35.059331620143688 48.449504260343431, 35.059331620136234 48.44976669329327, 35.058935959663451 48.449766693295835))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "televezionnaya", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.05200743312669 48.443792557693918, 35.052007433127628 48.443727086838358, 35.052106129702281 48.443727086841122, 35.052106129705528 48.443792557695382, 35.05200743312669 48.443792557693918))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "kuri", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.050450858264746 48.440844838909406, 35.050450858262167 48.440790634115992, 35.050532566646758 48.440790634116794, 35.05053256664872 48.440844838911367, 35.050450858264746 48.440844838909406))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });
            this.stops.Add(
                new Stop
                    {
                        Name = "lazariana", 
                        StopGeography =
                            SqlGeography.STGeomFromText(
                                new SqlChars(
                            "POLYGON ((35.048426411509944 48.438419103271258, 35.048426411510405 48.438347925472577, 35.0485336998809 48.43834792547451, 35.048533699879179 48.438419103270796, 35.048426411509944 48.438419103271258))"), 
                                4326).EnvelopeCenter(), 
                        StopType = 2
                    });

            // fake routes
            var routeA =
                SqlGeography.STGeomFromText(
                    new SqlChars(
                        "LINESTRING (35.045555932160916 48.46446173324123, 35.04563 48.464420000000004, 35.04647 48.464000000000006, 35.04751 48.463440000000006, 35.048300000000005 48.463060000000006, 35.04887 48.462750000000007, 35.049240000000005 48.462540000000004, 35.05021 48.46202, 35.0506 48.461810000000007, 35.050850000000004 48.46166, 35.051750000000006 48.461180000000006, 35.05319 48.46043, 35.05387 48.460060000000006, 35.054390000000005 48.45978, 35.054640000000006 48.459660000000007, 35.05621 48.45882, 35.05754 48.4581, 35.058730000000004 48.457490000000007, 35.059380000000004 48.45714, 35.06004 48.4568, 35.06134 48.45615, 35.06134 48.45615, 35.060700000000004 48.45559, 35.059830000000005 48.4549, 35.05946 48.454600000000006, 35.058530000000005 48.453860000000006, 35.058 48.453410000000005, 35.057320000000004 48.452850000000005, 35.056960000000004 48.45257, 35.0563 48.45206, 35.055800000000005 48.451660000000004, 35.055730000000004 48.451570000000004, 35.055730000000004 48.451570000000004, 35.05756 48.450590000000005, 35.058530000000005 48.450050000000005, 35.05942 48.44955, 35.05942 48.44955, 35.059000000000005 48.44919, 35.059000000000005 48.44919, 35.058840000000004 48.44912, 35.05809 48.44854, 35.05772 48.44825, 35.0574 48.44798, 35.057010000000005 48.447680000000005, 35.056740000000005 48.447480000000006, 35.055710000000005 48.44662, 35.05532 48.446320000000007, 35.05485 48.445940000000007, 35.0544 48.44561, 35.053990000000006 48.44527, 35.05341 48.444820000000007, 35.053000000000004 48.444500000000005, 35.05238 48.444010000000006, 35.05203 48.44373, 35.051790000000004 48.44355, 35.051280000000006 48.443140000000007, 35.050940000000004 48.442890000000006, 35.050830000000005 48.442780000000006, 35.05075 48.4427, 35.05071 48.44261, 35.05069 48.442510000000006, 35.050670000000004 48.442310000000006, 35.05064 48.44205, 35.05057 48.44151, 35.05055 48.44118, 35.0505 48.440810000000006, 35.050450000000005 48.4406, 35.05039 48.44048, 35.050290000000004 48.440340000000006, 35.050180000000005 48.44019, 35.04997 48.439960000000006, 35.049580000000006 48.43954, 35.048930000000006 48.43885, 35.048770000000005 48.438680000000005, 35.048473530404848 48.438382342777047)"), 
                    4326);
            var routeB =
                SqlGeography.STGeomFromText(
                    new SqlChars(
                        "LINESTRING (35.038610000000006 48.42887, 35.038810000000005 48.428950000000007, 35.039500000000004 48.4292, 35.039730000000006 48.42931, 35.040090000000006 48.42949, 35.04061 48.429880000000004, 35.04128 48.43043, 35.04166 48.430740000000007, 35.04211 48.431120000000007, 35.0424 48.431390000000007, 35.04252 48.43153, 35.042910000000006 48.431920000000005, 35.04323 48.432280000000006, 35.043730000000004 48.43285, 35.04399 48.433130000000006, 35.044290000000004 48.433470000000007, 35.04453 48.43372, 35.044830000000005 48.434070000000006, 35.04574 48.435050000000004, 35.04646 48.43583, 35.047180000000004 48.436640000000004, 35.04755 48.437030000000007, 35.047850000000004 48.43735, 35.04831 48.437880000000007, 35.048640000000006 48.438300000000005, 35.048640000000006 48.438300000000005, 35.04887 48.438570000000006, 35.049080000000004 48.438770000000005, 35.049710000000005 48.439490000000006, 35.050110000000004 48.43992, 35.05028 48.4401, 35.0504 48.440290000000005, 35.050520000000006 48.44046, 35.050560000000004 48.440560000000005, 35.05062 48.440760000000004, 35.05066 48.44111, 35.050740000000005 48.441730000000007, 35.0508 48.442310000000006, 35.050810000000006 48.4425, 35.050850000000004 48.44259, 35.05088 48.442670000000007, 35.050940000000004 48.442750000000004, 35.051010000000005 48.44283, 35.05111 48.442910000000005, 35.05125 48.443020000000004, 35.051410000000004 48.443140000000007, 35.05165 48.44333, 35.052110000000006 48.443700000000007, 35.0531 48.44445, 35.053490000000004 48.44474, 35.05395 48.445110000000007, 35.05442 48.44548, 35.054770000000005 48.44577, 35.05518 48.4461, 35.05552 48.446380000000005, 35.055780000000006 48.446580000000004, 35.05675 48.447370000000006, 35.056830000000005 48.447440000000007, 35.057140000000004 48.44769, 35.057550000000006 48.447990000000004, 35.05789 48.448260000000005, 35.058170000000004 48.44848, 35.05845 48.448710000000005, 35.058910000000004 48.449070000000006, 35.059000000000005 48.44919, 35.05942 48.44955, 35.05946 48.44959, 35.05953 48.449630000000006, 35.060480000000005 48.45042, 35.06103 48.45087, 35.06174 48.45147, 35.06223 48.4519, 35.063010000000006 48.452580000000005, 35.06355 48.45306, 35.064040000000006 48.45347, 35.064440000000005 48.453820000000007, 35.06494 48.454260000000005, 35.06544 48.454710000000006, 35.06544 48.454710000000006, 35.063840000000006 48.455520000000007, 35.063340000000004 48.45577, 35.062900000000006 48.455990000000007, 35.062520000000006 48.456170000000007, 35.06223 48.456320000000005, 35.06201 48.4564, 35.061780000000006 48.45651, 35.0615 48.456640000000007, 35.059830000000005 48.457510000000006, 35.05841 48.458220000000004, 35.05801 48.45846, 35.057080000000006 48.45893, 35.056560000000005 48.45922, 35.05595 48.45956, 35.05537 48.459880000000005, 35.05507 48.46003, 35.05489 48.46012, 35.05458 48.46027, 35.05286 48.46117, 35.05131 48.462, 35.05022 48.4626, 35.04965 48.462900000000005, 35.04894 48.46325, 35.048170000000006 48.463690000000007, 35.04793 48.463800000000006, 35.0465 48.46457, 35.04543 48.46511, 35.04453 48.4656)"), 
                    4326);

            var type = new TransportType { Id = 1, Type = "Bus" };
            this.Routes.Add(
                new Route
                    {
                        Id = 1, 
                        Name = "53A", 
                        RouteGeography = routeA, 
                        RouteType = (int)Transport.Bus, 
                        Stops = this.stops, 
                        Speed = 21, 
                        Price = 3, 
                        Type = type, 
                        WalkingRoutes = new List<WalkingRoute>()
                    });

            /*   this.Routes.Add(new Route
                {
                    Id = 2, Name = "53B",
                    RouteGeography = routeB,
                    RouteType = (int)Transport.Bus
                });*/

            // fake feedbacks
            this.Feedbacks.Add(new Feedback { Id = 1, Name = "Fred", Text = "Allgood", Type = 1 });
            this.Feedbacks.Add(new Feedback { Id = 2, Name = "Fred1", Text = "Allgood1", Type = 2 });
            this.Feedbacks.Add(new Feedback { Id = 3, Name = "Fred2", Text = "Allgood2", Type = 0 });

            // fake transport types
            this.TransportTypes.Add(new TransportType { Type = "Ttype1" });
            this.TransportTypes.Add(new TransportType { Type = "Ttype2" });
            this.TransportTypes.Add(new TransportType { Type = "Ttype3" });

            // fake languages
            this.Languages.Add(new Language { Id = 1, Name = "Russian" });
            this.Languages.Add(new Language { Id = 2, Name = "Ukrainian" });
            this.Languages.Add(new Language { Id = 3, Name = "English" });

            // fake places
            this.Places.Add(
                new Place { Id = 1, Name = "artema", Count = 5, place = new Place(), LangId = 1, Type = "улица", });
            this.Places.Add(
                new Place { Id = 2, Name = "barikadnaya", Count = 3, place = new Place(), LangId = 1, Type = "улица", });
            this.Places.Add(
                new Place { Id = 3, Name = "klari", Count = 2, place = new Place(), LangId = 1, Type = "проспект", });

            // fake buildings
            this.Buildings.Add(
                new Building
                    {
                       Id = 1, Number = "50", BuildingIndexNumber = "a", Place = new Place(), PlaceId = 1, Count = 4, 
                    });
            this.Buildings.Add(
                new Building
                    {
                       Id = 2, Number = "55", BuildingIndexNumber = string.Empty, Place = new Place(), Count = 4, 
                    });
            this.Buildings.Add(
                new Building { Id = 3, Number = "60", BuildingIndexNumber = "b", Place = new Place(), Count = 10, });
        }

        #endregion
    }
}