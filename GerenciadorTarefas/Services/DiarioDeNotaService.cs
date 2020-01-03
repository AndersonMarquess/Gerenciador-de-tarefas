using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Models;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorTarefas.Services {
    public class DiarioDeNotaService : IDiarioDeNotaService {

        private readonly IDiarioDeNotaDAO _diarioDao;
        private readonly ITarefaDAO _tarefaDao;

        public DiarioDeNotaService(IDiarioDeNotaDAO diarioDao, ITarefaDAO tarefaDao) {
            _diarioDao = diarioDao;
            _tarefaDao = tarefaDao;
        }

        public void Atualizar(DiarioDeNota diario) {
            var diarioAntigo = BuscarPorId(diario.Id);
            if(diarioAntigo != null) {
                _diarioDao.Atualizar(diario);
            }
        }

        public DiarioDeNota BuscarPorId(int id) {
            return _diarioDao.BuscarPorId(id);
        }

        public IEnumerable<DiarioDeNota> BuscarDiariosDeTarefasEntregueDoAlunoComId(int id) {
            return _diarioDao.BuscarDiariosDeTarefasEntregueDoAlunoComId(id);
        }

        public void Cadastrar(DiarioDeNota diario) {
            _diarioDao.Cadastrar(diario);
        }

        public void RemoverPorId(int id) {
            var diario = BuscarPorId(id);
            if(diario != null) {
                _diarioDao.Remover(diario);
            }
        }

        public IEnumerable<Tarefa> BuscarTarefasNaoEntregueDoAlunoComId(int id) {
            return _diarioDao.BuscarTarefasNaoEntreguesDoAlunoComId(id);
        }
    }
}