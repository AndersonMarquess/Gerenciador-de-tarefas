using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO {
    public interface IAdministradorDAO {

        Administrador FindByCredenciais(string login);

        void Insert(Administrador admin);

        void Update(Administrador admin);

        void UpdateSenha(Administrador admin);

        void Delete(int id);
    }
}
