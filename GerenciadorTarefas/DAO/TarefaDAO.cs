using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO
{
    public class TarefaDAO : ITarefaDAO {

        private ConexaoDB dao = ConexaoDB.getInstance();

        public void concluir(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"UPDATE Tarefas SET Concluido = @Concluido WHERE Id = @id";
                command.Parameters.AddWithValue("@Concluido", 1);
                command.Parameters.AddWithValue("@id", id);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public void delete(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"DELETE FROM Tarefas WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public List<Tarefa> findAll(int idAdmin) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Tarefas WHERE IdAdmin = @idAdmin AND Concluido = 0";
                command.Parameters.AddWithValue("@idAdmin", idAdmin);
                return dao.queryListaTarefa(command);
            } catch(Exception) {
                return null;
            }
        }

        public List<Tarefa> findAllByTipo(TipoTarefa tipo) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Tarefas WHERE TipoDaTarefa = @tipo AND Concluido = 0";
                command.Parameters.AddWithValue("@tipo", tipo.ToString());
                return dao.queryListaTarefa(command);
            } catch(Exception) {
                return null;
            }
        }

        public Tarefa findById(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Tarefas WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                return dao.queryListaTarefa(command).First();
            } catch(Exception) {
                return null;
            }

        }

        public void insert(Tarefa tarefa) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Tarefas (TipoDaTarefa, DataLimite, Descricao, IdAdmin) VALUES (@tipo, @dataLimite, @descricao, @idAdmin)";
                command.Parameters.AddWithValue("@tipo", tarefa.TipoDaTarefa.ToString());
                command.Parameters.AddWithValue("@dataLimite", tarefa.getDataFormatada());
                command.Parameters.AddWithValue("@descricao", tarefa.Descricao);
                command.Parameters.AddWithValue("@idAdmin", tarefa.IdAdmin);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public void update(Tarefa tarefa) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"UPDATE Tarefas SET TipoDaTarefa = @tipo, DataLimite = @dataLimite, Descricao = @descricao WHERE Id = @id";
                command.Parameters.AddWithValue("@tipo", tarefa.TipoDaTarefa.ToString());
                command.Parameters.AddWithValue("@dataLimite", tarefa.getDataFormatada());
                command.Parameters.AddWithValue("@descricao", tarefa.Descricao);
                command.Parameters.AddWithValue("@id", tarefa.Id);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }
    }
}