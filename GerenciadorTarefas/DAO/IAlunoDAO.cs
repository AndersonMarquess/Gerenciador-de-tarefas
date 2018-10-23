using GerenciadorTarefas.Models;
using System.Collections.Generic;

namespace GerenciadorTarefas.DAO
{
    interface IAlunoDAO {
        void insert(Aluno aluno);

        void update(Aluno aluno);

        void delete(int id);

        List<Aluno> findAll();
    }
}
