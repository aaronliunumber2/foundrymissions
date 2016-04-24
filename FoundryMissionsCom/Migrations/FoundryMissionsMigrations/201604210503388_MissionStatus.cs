namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "Status", c => c.Int(nullable: false, defaultValue: 0));
            DropColumn("dbo.Missions", "Approved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "Approved", c => c.Boolean(nullable: false));
            DropColumn("dbo.Missions", "Status");
        }
    }
}
