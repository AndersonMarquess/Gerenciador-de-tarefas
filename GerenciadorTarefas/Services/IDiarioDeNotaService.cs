using GerenciadorTarefas.Models;
using System.Collections.Generic;

namespace GerenciadorTarefas.Services {
    public interface IDiarioDeNotaService {

        IEnumerable<DiarioDeNota> BuscarDiariosDeTarefasEntregueDoAlunoComId(int id);
        
        IEnumerable<Tarefa> BuscarTarefasNaoEntregueDoAlunoComId(int id);

        void Cadastrar(DiarioDeNota diario);

        void Atualizar(DiarioDeNota diario);

        void RemoverPorId(int id);

        DiarioDeNota BuscarPorId(int id);
    }
}
