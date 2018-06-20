namespace GestaoDinheiro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMov : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movimentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataTime = c.DateTime(nullable: false),
                        Descricao = c.String(nullable: false),
                        Valor = c.Double(nullable: false),
                        Saldo_Atual = c.Double(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Dono = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movimentoes");
        }
    }
}
