using GerenciadorTarefas.Models;
using System;
using System.Data.OleDb;

namespace GerenciadorTarefas.DAO
{
    public class AdministradorDAO : IAdministradorDAO
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

        public Administrador findByCredenciais(string login) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Administradores WHERE Login = @login";
                command.Parameters.AddWithValue("@login", login);

                return dao.queryAdministrador(command);
            } catch(Exception) {
                return null;
            }
        }

        public void insert(Administrador admin) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Administradores (Nome, Login, Senha, PalavraBackup) VALUES (@nome, @login, @senha, @palavra)";
                command.Parameters.AddWithValue("@nome", admin.Nome);
                command.Parameters.AddWithValue("@login", admin.Login);
                command.Parameters.AddWithValue("@senha", admin.Senha);
                command.Parameters.AddWithValue("@palavra", admin.PalavraBackup);

                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public void update(Administrador admin) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"UPDATE Administradores SET Nome = @nome, Login = @login, Senha = @senha";
                command.Parameters.AddWithValue("@nome", admin.Nome);
                command.Parameters.AddWithValue("@login", admin.Login);
                command.Parameters.AddWithValue("@senha", admin.Senha);

                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public void updateSenha(Administrador admin) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"UPDATE Administradores SET Senha = @senha";
                command.Parameters.AddWithValue("@senha", admin.Senha);

                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }
    }
}