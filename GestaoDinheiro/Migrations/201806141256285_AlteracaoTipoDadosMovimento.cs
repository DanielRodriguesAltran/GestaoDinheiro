namespace GestaoDinheiro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoTipoDadosMovimento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movimentoes", "DataTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movimentoes", "Valor", c => c.Double(nullable: false));
            AlterColumn("dbo.Movimentoes", "Saldo_Atual", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movimentoes", "Saldo_Atual", c => c.String());
            AlterColumn("dbo.Movimentoes", "Valor", c => c.String(nullable: false));
            AlterColumn("dbo.Movimentoes", "DataTime", c => c.String());
        }
    }
}
