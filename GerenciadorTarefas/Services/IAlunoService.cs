using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorTarefas.Services {
    public interface IAlunoService {

        void Cadastrar(Aluno aluno);

        IEnumerable<Aluno> BuscarTodos();

        Aluno BuscarPorId(int idAluno);

        void AdicionarFaltaAoAlunoComId(int id);

        IEnumerable<DiarioDePresenca> BuscarFaltasDoAlunoComId(int id);

        void RemoverFaltaDoAlunoComId(int id, string dataFalta);

        bool Atualizar(Aluno aluno);

        void Remover(int id);
    }
}
