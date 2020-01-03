using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.Services {
    public class AdministradorService : IAdministradorService {

        private readonly IAdministradorDAO _adminDao;

        public AdministradorService(IAdministradorDAO adminDao) {
            _adminDao = adminDao;
        }

        public bool Cadastrar(Administrador administrador) {
            if(LoginExiste(administrador)) {
                return false;
            } else {
                _adminDao.Cadastrar(administrador);
                return true;
            }
        }

        public Administrador Entrar(string login, string senha) {
            var admin = _adminDao.BuscarPorLogin(login);
            var senhaValida = BCrypt.Net.BCrypt.Verify(senha, admin.Senha);

            if(senhaValida) {
                return admin;
            }
            return null;
        }

        public bool AtualizarSenha(Administrador administrador) {
            if(LoginExiste(administrador)) {
                _adminDao.AtualizarSenha(administrador);
                return true;
            } else {
                return false;
            }
        }

        private bool LoginExiste(Administrador administrador) {
            return _adminDao.BuscarPorLogin(administrador.Login) != null;
        }
    }
}
