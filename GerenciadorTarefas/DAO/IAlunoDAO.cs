﻿using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO
{
    interface IAlunoDAO {
        Aluno findByCredenciais(string login);

        void insert(Aluno aluno);

        void update(Aluno aluno);

        void delete(int id);
    }
}