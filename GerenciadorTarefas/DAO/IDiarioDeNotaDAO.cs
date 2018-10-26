using GerenciadorTarefas.Models;
using System.Collections.Generic;

namespace GerenciadorTarefas.DAO
{
    interface IDiarioDeNotaDAO
    {
        List<Tarefa> findTarefasNaoEntreguesDoAluno(int id);

        void insert(DiarioDeNota diario);
    }
}
