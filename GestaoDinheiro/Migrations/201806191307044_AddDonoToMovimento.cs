namespace GestaoDinheiro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDonoToMovimento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movimentoes", "Dono", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movimentoes", "Dono", c => c.String());
        }
    }
}
