namespace GerenciadorTarefas.Migrations
{
    using GerenciadorTarefas.DAO;
    using GerenciadorTarefas.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GerenciadorTarefas.DAO.GerenciadorTarefasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GerenciadorTarefas.DAO.GerenciadorTarefasContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            AdicionarAdminPadrao(context);
        }

        private void AdicionarAdminPadrao(GerenciadorTarefasContext context) {
            var admin = new Administrador("admin", "admin", "admin", "admin");
            context.Administrador.AddOrUpdate(admin);
            context.SaveChanges();
        }
    }
}
