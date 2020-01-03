using GerenciadorTarefas.Models;
using MySql.Data.Entity;
using System.Data.Entity;

namespace GerenciadorTarefas.DAO {

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GerenciadorTarefasContext : DbContext {

        public GerenciadorTarefasContext() : base("MySqlPath") { }

        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<DiarioDePresenca> DiarioDePresenca { get; set; }
        public DbSet<DiarioDeNota> DiarioDeNota { get; set; }
    }
}