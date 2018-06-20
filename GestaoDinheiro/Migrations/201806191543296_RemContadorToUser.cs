namespace GestaoDinheiro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemContadorToUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Contador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Contador", c => c.Int(nullable: false));
        }
    }
}
