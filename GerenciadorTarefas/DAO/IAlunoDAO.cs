using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO
{
    interface IAlunoDAO    {
        void insert(Aluno aluno);

        void update(Aluno aluno);

        void delete(int id);
    }
}
