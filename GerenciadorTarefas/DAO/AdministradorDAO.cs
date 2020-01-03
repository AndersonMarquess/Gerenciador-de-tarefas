using GerenciadorTarefas.Models;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GerenciadorTarefas.DAO {
    public class AdministradorDAO : IAdministradorDAO {

        private readonly GerenciadorTarefasContext _context;

        public AdministradorDAO(GerenciadorTarefasContext context) {
            _context = context;
        }

        public void Remover(Administrador admin) {
            _context.Administrador.Remove(admin);
            _context.SaveChanges();
        }

        public Administrador BuscarPorId(int id) {
            return _context.Administrador.FirstOrDefault(a => a.Id == id);
        }

        public Administrador BuscarPorLogin(string login) {
            return _context.Administrador.Where(a => a.Login.Equals(login)).FirstOrDefault();
        }

        public void Cadastrar(Administrador admin) {
            _context.Administrador.Add(admin);
            _context.SaveChanges();
        }

        public void Atualizar(Administrador admin) {
            _context.Administrador.AddOrUpdate(admin);
            _context.SaveChanges();
        }

        public void AtualizarSenha(Administrador admin) {
            var adminAntigo = BuscarPorLogin(admin.Login);
            adminAntigo.Senha = admin.Senha;
            _context.SaveChanges();
        }
    }
}
