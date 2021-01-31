namespace Finalni_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaposlens", "GodinaRodjenja", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposlens", "GodinaRodjenja", c => c.Int(nullable: false));
        }
    }
}
