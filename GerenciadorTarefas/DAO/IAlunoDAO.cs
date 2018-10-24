using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorTarefas.DAO
{
    interface IAlunoDAO {
        void insert(Aluno aluno);

        void update(Aluno aluno);

        void delete(int id);

        List<Aluno> findAll();

        Aluno findById(int id);

        HashSet<DiarioDePresenca> findAllFaltasByAlunoId(int id);

        void addFaltaAluno(DiarioDePresenca diario);

        void removerFaltaByAlunoId(int id, DateTime data);
    }
}
