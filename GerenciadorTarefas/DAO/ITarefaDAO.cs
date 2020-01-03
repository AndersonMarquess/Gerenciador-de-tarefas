using GerenciadorTarefas.Models;
using System.Collections.Generic;

namespace GerenciadorTarefas.DAO {
    public interface ITarefaDAO {

        List<Tarefa> BuscarTodasPorTipo(TipoTarefa tipo);

        List<Tarefa> BuscarTodasPorAdmin(int id);

        Tarefa BuscarPorId(int id);

        void Cadastrar(Tarefa tarefa);

        void Atualizar(Tarefa tarefa);

        void RemoverPorId(int id);

        IEnumerable<Tarefa> BuscarTodas();
    }
}