using GerenciadorTarefas.Models;
using System.Collections.Generic;

namespace GerenciadorTarefas.Services {
    public interface ITarefaService {

        IEnumerable<Tarefa> BuscarTodasPorTipo(string tipo);

        IEnumerable<Tarefa> BuscarTodasPorIdAdmin(int idAdmin);

        Tarefa BuscarPorId(int idTarefa);

        bool Cadastrar(Tarefa tarefa);

        void Concluir(int idTarefa);

        bool Atualizar(Tarefa tarefa);

        void RemoverPorId(int id);
    }
}
