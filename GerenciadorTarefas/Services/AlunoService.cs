using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorTarefas.Services {
    public class AlunoService : IAlunoService {

        private readonly IAlunoDAO _alunoDao;

        public AlunoService(IAlunoDAO alunoDao) {
            _alunoDao = alunoDao;
        }

        public void AdicionarFaltaAoAlunoComId(int id) {
            var aluno = BuscarPorId(id);
            if(aluno != null) {
                var falta = new DiarioDePresenca() { IdAluno = aluno.Id, DataDaFalta = DateTime.Today };
                _alunoDao.AdicionarFalta(falta);
            }
        }

        public IEnumerable<DiarioDePresenca> BuscarFaltasDoAlunoComId(int id) {
            return _alunoDao.BuscarFaltasDoAlunoComId(id);
        }

        public void RemoverFaltaDoAlunoComId(int id, string dataFalta) {
            var data = DateTime.Parse(dataFalta);
            _alunoDao.RemoverFaltaDoAlunoComId(id, data);
        }

        public bool Atualizar(Aluno aluno) {
            var alunoAntigo = BuscarPorId(aluno.Id);
            if(alunoAntigo != null) {
                _alunoDao.Atualizar(aluno);
                return true;
            } else {
                return false;
            }
        }

        public Aluno BuscarPorId(int idAluno) {
            if(idAluno <= 0) {
                return null;
            }
            return _alunoDao.BuscarPorId(idAluno);
        }

        public IEnumerable<Aluno> BuscarTodos() {
            return _alunoDao.BuscarTodos();
        }

        public void Cadastrar(Aluno aluno) {
            aluno.Id = 0;
            _alunoDao.Cadastrar(aluno);
        }

        public void Remover(int id) {
            var aluno = BuscarPorId(id);
            if(aluno != null) {
                _alunoDao.Remover(aluno);
            }
        }
    }
}
