namespace FoundryMissionsCom.Migrations.FoundryMissionsMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "MissionLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "MissionLink");
        }
    }
}
