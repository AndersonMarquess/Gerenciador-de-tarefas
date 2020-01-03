using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO {
    public interface IAdministradorDAO {

        Administrador BuscarPorLogin(string login);
        
        Administrador BuscarPorId(int id);
        
        void Cadastrar(Administrador admin);

        void Atualizar(Administrador admin);

        void AtualizarSenha(Administrador admin);

        void Remover(Administrador admin);
    }
}
