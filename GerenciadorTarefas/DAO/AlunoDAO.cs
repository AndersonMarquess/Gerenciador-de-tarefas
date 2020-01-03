using GerenciadorTarefas.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace GerenciadorTarefas.DAO {

    public class AlunoDAO : IAlunoDAO {

        private readonly GerenciadorTarefasContext _context;

        public AlunoDAO(GerenciadorTarefasContext context) {
            _context = context;
        }

        public void AdicionarFalta(DiarioDePresenca diario) {
            _context.DiarioDePresenca.Add(diario);
            _context.SaveChanges();
        }

        public IEnumerable<DiarioDePresenca> BuscarFaltasDoAlunoComId(int id) {
            if(_context.DiarioDePresenca.Any(d => d.IdAluno == id)) {
                return _context.DiarioDePresenca.Where(d => d.IdAluno == id).ToList();
            } else {
                return new List<DiarioDePresenca>();
            }
        }

        public void RemoverFaltaDoAlunoComId(int id, DateTime data) {
            var diario = _context.DiarioDePresenca
                .FirstOrDefault(d => d.IdAluno == id && d.DataDaFalta == data);
            if(diario != null) {
                _context.DiarioDePresenca.Remove(diario);
                _context.SaveChanges();
            }
        }

        public void Atualizar(Aluno aluno) {
            _context.Aluno.AddOrUpdate(aluno);
            _context.Endereco.AddOrUpdate(aluno.Endereco);
            _context.SaveChanges();
        }

        public Aluno BuscarPorId(int id) {
            return _context.Aluno.Include(a => a.Endereco).FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Aluno> BuscarTodos() {
            return _context.Aluno.ToList();
        }

        public void Cadastrar(Aluno aluno) {
            _context.Aluno.Add(aluno);
            _context.Endereco.Add(aluno.Endereco);
            _context.SaveChanges();
        }

        public void Remover(Aluno aluno) {
            _context.Aluno.Remove(aluno);
            _context.SaveChanges();
        }
    }
}