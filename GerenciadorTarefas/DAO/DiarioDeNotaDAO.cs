using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO {
    public class DiarioDeNotaDAO : IDiarioDeNotaDAO {

        private readonly GerenciadorTarefasContext _context;

        public DiarioDeNotaDAO(GerenciadorTarefasContext context) {
            _context = context;
        }

        public void Atualizar(DiarioDeNota diario) {
            _context.DiarioDeNota.AddOrUpdate(diario);
            _context.SaveChanges();
        }

        public DiarioDeNota BuscarPorId(int id) {
            return _context.DiarioDeNota.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<DiarioDeNota> BuscarDiariosDeTarefasEntregueDoAlunoComId(int idAluno) {
            if(_context.DiarioDeNota.Any(d => d.IdAluno == idAluno)) {
                return _context.DiarioDeNota.Where(d => d.IdAluno == idAluno).ToList();
            } else {
                return new List<DiarioDeNota>();
            }
        }

        public IEnumerable<Tarefa> BuscarTarefasNaoEntreguesDoAlunoComId(int id) {
            var tarefas = _context.Tarefa.ToList();
            var tarefasEntregues = BuscarDiariosDeTarefasEntregueDoAlunoComId(id);

            return tarefas.Where(t => !tarefasEntregues.Any(diario => diario.IdTarefa == t.Id)).ToList();
        }

        public void Cadastrar(DiarioDeNota diario) {
            _context.DiarioDeNota.Add(diario);
            _context.SaveChanges();
        }

        public void Remover(DiarioDeNota diario) {
            _context.DiarioDeNota.Remove(diario);
            _context.SaveChanges();
        }
    }
}