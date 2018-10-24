using GerenciadorTarefas.Models;
using System.Collections.Generic;

namespace GerenciadorTarefas.DAO
{
    interface ITarefaDAO {

        List<Tarefa> findAllByTipo(TipoTarefa tipo, int id);

        List<Tarefa> findAll(int id);

        Tarefa findById(int id);

        void insert(Tarefa tarefa);

        void update(Tarefa tarefa);

        void delete(int id);

        List<DiarioDeNota> findAllByAlunoId(int id);
    }
}