using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorTarefas.DAO {
    public interface IAlunoDAO {

        void Cadastrar(Aluno aluno);

        void Atualizar(Aluno aluno);

        void Remover(Aluno aluno);

        IEnumerable<Aluno> BuscarTodos();

        Aluno BuscarPorId(int id);

        IEnumerable<DiarioDePresenca> BuscarFaltasDoAlunoComId(int id);

        void AdicionarFalta(DiarioDePresenca diario);

        void RemoverFaltaDoAlunoComId(int id, DateTime dataDaFalta);
    }
}
