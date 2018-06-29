namespace GestaoDinheiro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartamento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimentoes", "Departamento", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movimentoes", "Departamento");
        }
    }
}
