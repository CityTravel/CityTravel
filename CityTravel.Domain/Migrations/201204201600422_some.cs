namespace CityTravel.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class some : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Building",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingBin = c.Binary(),
                        BuildingIndexNumber = c.String(),
                        Count = c.Int(nullable: false),
                        Number = c.String(),
                        PlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Place", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "Place",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(),
                        LangId = c.Int(),
                        Name = c.String(),
                        PlaceBin = c.Binary(),
                        PlaceInRussainId = c.Int(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Place", t => t.LangId)
                .Index(t => t.LangId);
            
            CreateTable(
                "Feedback",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Name = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Route",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RouteBin = c.Binary(),
                        RouteType = c.Int(),
                        WaitingTime = c.Time(nullable: false),
                        Price = c.Single(nullable: false),
                        Speed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("TransportType", t => t.RouteType)
                .Index(t => t.RouteType);
            
            CreateTable(
                "Stop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StopBin = c.Binary(),
                        StopType = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("TransportType", t => t.StopType)
                .Index(t => t.StopType);
            
            CreateTable(
                "TransportType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "InvalidDirection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Direction = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "InvalidCharacters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValidWord = c.String(),
                        InvalidWord = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "StopRoutes",
                c => new
                    {
                        Stop_Id = c.Int(nullable: false),
                        Route_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Stop_Id, t.Route_Id })
                .ForeignKey("Stop", t => t.Stop_Id, cascadeDelete: true)
                .ForeignKey("Route", t => t.Route_Id, cascadeDelete: true)
                .Index(t => t.Stop_Id)
                .Index(t => t.Route_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("StopRoutes", new[] { "Route_Id" });
            DropIndex("StopRoutes", new[] { "Stop_Id" });
            DropIndex("Stop", new[] { "StopType" });
            DropIndex("Route", new[] { "RouteType" });
            DropIndex("Place", new[] { "LangId" });
            DropIndex("Building", new[] { "PlaceId" });
            DropForeignKey("StopRoutes", "Route_Id", "Route");
            DropForeignKey("StopRoutes", "Stop_Id", "Stop");
            DropForeignKey("Stop", "StopType", "TransportType");
            DropForeignKey("Route", "RouteType", "TransportType");
            DropForeignKey("Place", "LangId", "Place");
            DropForeignKey("Building", "PlaceId", "Place");
            DropTable("StopRoutes");
            DropTable("InvalidCharacters");
            DropTable("InvalidDirection");
            DropTable("TransportType");
            DropTable("Stop");
            DropTable("Route");
            DropTable("Language");
            DropTable("Feedback");
            DropTable("Place");
            DropTable("Building");
        }
    }
}
