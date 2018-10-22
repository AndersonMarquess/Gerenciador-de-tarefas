using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO
{
    interface IAdministradorDAO {
        Administrador findByCredenciais(string login);

        void insert(Administrador admin);

        void update(Administrador admin);

        void updateSenha(Administrador admin);

        void delete(int id);
    }
}