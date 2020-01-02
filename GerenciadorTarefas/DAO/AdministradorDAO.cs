using GerenciadorTarefas.Models;
using System.Linq;

namespace GerenciadorTarefas.DAO {
    public class AdministradorDAO : IAdministradorDAO {

        private readonly GerenciadorTarefasContext _context;

        public AdministradorDAO(GerenciadorTarefasContext context) {
            _context = context;
        }

        public void Delete(int id) {
            var admin = FindById(id);
            if(admin != null) {
                _context.Administrador.Remove(admin);
                _context.SaveChanges();
            }
        }

        private Administrador FindById(int id) {
            return _context.Administrador.FirstOrDefault(a => a.Id == id);
        }

        public Administrador FindByCredenciais(string login) {
            return _context.Administrador.Where(a => a.Login.Equals(login)).FirstOrDefault();
        }

        public void Insert(Administrador admin) {
            _context.Administrador.Add(admin);
            _context.SaveChanges();
        }

        public void Update(Administrador admin) {
            var adminAntigo = FindById(admin.Id);
            adminAntigo.Login = admin.Login;
            adminAntigo.Nome = admin.Nome;
            adminAntigo.PalavraBackup = admin.PalavraBackup;
            adminAntigo.Senha = admin.Senha;
            _context.SaveChanges();
        }

        public void UpdateSenha(Administrador admin) {
            var adminAntigo = FindByCredenciais(admin.Login);
            adminAntigo.Senha = admin.Senha;
            _context.SaveChanges();
        }
    }
}
