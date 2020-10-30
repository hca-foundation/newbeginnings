namespace TNBCSurvey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Andrew3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ProgramStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clients", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Clients", "GroupNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Clients", "Phone");
            DropColumn("dbo.Clients", "Survey_Status");
            DropColumn("dbo.SurveyTickets", "SurveyPeriod");
            DropColumn("dbo.SurveyTickets", "SubmittedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyTickets", "SubmittedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SurveyTickets", "SurveyPeriod", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "Survey_Status", c => c.String());
            AddColumn("dbo.Clients", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.Clients", "GroupNumber", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Clients", "Active");
            DropColumn("dbo.Clients", "ProgramStartDate");
        }
    }
}
