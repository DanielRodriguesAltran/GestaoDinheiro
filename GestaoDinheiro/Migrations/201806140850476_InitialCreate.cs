namespace GestaoDinheiro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movimentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataTime = c.String(),
                        Local = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                        Valor = c.String(nullable: false),
                        Saldo_Atual = c.String(),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movimentoes");
        }
    }
}
