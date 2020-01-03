using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorTarefas.Services {
    public class TarefaService : ITarefaService {

        private readonly ITarefaDAO _tarefaDao;

        public TarefaService(ITarefaDAO tarefaDao) {
            _tarefaDao = tarefaDao;
        }

        public bool Atualizar(Tarefa tarefa) {
            var tarefaAntiga = BuscarPorId(tarefa.Id);
            if(tarefaAntiga != null && PossuiDataValida(tarefa)) {
                _tarefaDao.Atualizar(tarefa);
                return true;
            } else {
                return false;
            }
        }

        private bool PossuiDataValida(Tarefa tarefa) {
            return tarefa.DataLimite >= DateTime.Today;
        }

        public IEnumerable<Tarefa> BuscarTodasPorIdAdmin(int idAdmin) {
            return _tarefaDao.BuscarTodasPorAdmin(idAdmin);
        }

        public IEnumerable<Tarefa> BuscarTodasPorTipo(string tipo) {
            TipoTarefa tipoDaTarefa;
            Enum.TryParse(tipo, true, out tipoDaTarefa);
            return _tarefaDao.BuscarTodasPorTipo(tipoDaTarefa);
        }

        public bool Cadastrar(Tarefa tarefa) {
            if(PossuiDataValida(tarefa)) {
                _tarefaDao.Cadastrar(tarefa);
                return true;
            } else {
                return false;
            }
        }

        public void Concluir(int idTarefa) {
            var tarefa = BuscarPorId(idTarefa);
            if(tarefa != null) {
                tarefa.Andamento = AndamentoTarefa.Concluido;
                _tarefaDao.Atualizar(tarefa);
            }
        }

        public void RemoverPorId(int id) {
            _tarefaDao.RemoverPorId(id);
        }

        public Tarefa BuscarPorId(int idTarefa) {
            return _tarefaDao.BuscarPorId(idTarefa);
        }
    }
}