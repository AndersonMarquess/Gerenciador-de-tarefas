namespace GerenciadorTarefas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administradors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        Login = c.String(unicode: false),
                        Senha = c.String(unicode: false),
                        PalavraBackup = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Alunoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Matricula = c.Int(nullable: false),
                        Serie = c.String(unicode: false),
                        Telefone = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Nome = c.String(nullable: false, maxLength: 250, unicode: false, storeType: "nvarchar"),
                        Cpf = c.String(unicode: false),
                        Sexo = c.String(unicode: false),
                        DataNascimento = c.DateTime(nullable: false, precision: 0),
                        Endereco_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enderecoes", t => t.Endereco_Id);
            
            CreateTable(
                "dbo.DiarioDeNotas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdAluno = c.Int(nullable: false),
                        IdTarefa = c.Int(nullable: false),
                        NotaRecebida = c.Double(nullable: false),
                        Observacoes = c.String(unicode: false),
                        Aluno_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alunoes", t => t.Aluno_Id);

            CreateTable(
                "dbo.DiarioDePresencas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdAluno = c.Int(nullable: false),
                        DataDaFalta = c.DateTime(nullable: false, precision: 0),
                        Aluno_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alunoes", t => t.Aluno_Id);

            CreateTable(
                "dbo.Enderecoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(unicode: false),
                        Cep = c.String(maxLength: 9, unicode: false, storeType: "nvarchar"),
                        Numero = c.String(unicode: false),
                        Bairro = c.String(unicode: false),
                        Cidade = c.String(unicode: false),
                        Estado = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Alunoes", "Endereco_Id", "dbo.Enderecoes");
            DropForeignKey("dbo.DiarioDePresencas", "Aluno_Id", "dbo.Alunoes");
            DropForeignKey("dbo.DiarioDeNotas", "Aluno_Id", "dbo.Alunoes");
            DropIndex("dbo.Alunoes", new[] { "Endereco_Id" });
            DropIndex("dbo.DiarioDePresencas", new[] { "Aluno_Id" });
            DropIndex("dbo.DiarioDeNotas", new[] { "Aluno_Id" });
            DropTable("dbo.Tarefas");
            DropTable("dbo.Enderecoes");
            DropTable("dbo.DiarioDePresencas");
            DropTable("dbo.DiarioDeNotas");
            DropTable("dbo.Alunoes");
            DropTable("dbo.Administradors");
        }
    }
}
