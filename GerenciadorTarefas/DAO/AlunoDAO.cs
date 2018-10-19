using GerenciadorTarefas.Models;
using System;
using System.Data.OleDb;

namespace GerenciadorTarefas.DAO
{
    public class AlunoDAO : IAlunoDAO
    {

        private ConexaoDB dao = ConexaoDB.getInstance();

        public void delete(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"DELETE FROM Alunos WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);

                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public Aluno findByCredenciais(string login) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Alunos WHERE Login = @login";
                command.Parameters.AddWithValue("@login", login);

                return dao.queryAluno(command);
            } catch(Exception) {
                return null;
            }
        }

        public void insert(Aluno aluno) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Alunos (Nome, Login, Senha) VALUES (@nome, @login, @senha)";
                command.Parameters.AddWithValue("@nome", aluno.Nome);
                command.Parameters.AddWithValue("@login", aluno.Login);
                command.Parameters.AddWithValue("@senha", aluno.Senha);

                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public void update(Aluno aluno) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"UPDATE Alunos SET Nome = @nome, Login = @login, Senha = @senha";
                command.Parameters.AddWithValue("@nome", aluno.Nome);
                command.Parameters.AddWithValue("@login", aluno.Login);
                command.Parameters.AddWithValue("@senha", aluno.Senha);

                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }
    }
}