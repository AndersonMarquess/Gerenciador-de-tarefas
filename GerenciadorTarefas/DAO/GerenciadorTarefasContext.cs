using GerenciadorTarefas.Models;
using MySql.Data.Entity;
using System.Data.Entity;

namespace GerenciadorTarefas.DAO {

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GerenciadorTarefasContext : DbContext {

        public GerenciadorTarefasContext() : base("MySqlPath") { }

        public DbSet<Administrador> Administrador { get; set; }
    }
}