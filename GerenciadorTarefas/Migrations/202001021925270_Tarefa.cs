namespace GerenciadorTarefas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tarefa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tarefas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoDaTarefa = c.Int(nullable: false),
                        DataLimite = c.DateTime(nullable: false, precision: 0),
                        Descricao = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        IdAdmin = c.Int(nullable: false),
                        Andamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tarefas");
        }
    }
}
