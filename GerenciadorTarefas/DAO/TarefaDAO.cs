using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO {
    public class TarefaDAO : ITarefaDAO {

        private readonly GerenciadorTarefasContext _context;

        public TarefaDAO(GerenciadorTarefasContext context) {
            _context = context;
        }

        public void RemoverPorId(int id) {
            var tarefa = BuscarPorId(id);
            if(tarefa != null) {
                _context.Tarefa.Remove(tarefa);
                _context.SaveChanges();
            }
        }

        public List<Tarefa> BuscarTodasPorAdmin(int idAdmin) {
            return _context.Tarefa
                .Where(t => t.IdAdmin == idAdmin && t.Andamento == AndamentoTarefa.Em_Andamento)
                .ToList();
        }

        public List<Tarefa> BuscarTodasPorTipo(TipoTarefa tipo) {
            return _context.Tarefa
                .Where(t => t.TipoDaTarefa == tipo && t.Andamento == AndamentoTarefa.Em_Andamento)
                .ToList();
        }

        public Tarefa BuscarPorId(int id) {
            return _context.Tarefa.FirstOrDefault(t => t.Id == id);
        }

        public void Cadastrar(Tarefa tarefa) {
            _context.Tarefa.Add(tarefa);
            _context.SaveChanges();
        }

        public void Atualizar(Tarefa tarefa) {
            _context.Tarefa.AddOrUpdate(tarefa);
            _context.SaveChanges();
        }

        public IEnumerable<Tarefa> BuscarTodas() {
            return _context.Tarefa.ToList();
        }
    }
}