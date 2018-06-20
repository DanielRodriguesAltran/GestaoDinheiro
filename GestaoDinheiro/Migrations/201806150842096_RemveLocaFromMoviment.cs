namespace GestaoDinheiro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemveLocaFromMoviment : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movimentoes", "Local");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movimentoes", "Local", c => c.String(nullable: false));
        }
    }
}
