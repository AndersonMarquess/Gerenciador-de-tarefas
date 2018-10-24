using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO
{
    public class TarefaDAO : ITarefaDAO {

        private ConexaoDB dao = ConexaoDB.getInstance();

        public void delete(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"DELETE FROM Tarefas WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public List<Tarefa> findAll(int idAluno) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Tarefas WHERE IdAluno = @idAluno";
                command.Parameters.AddWithValue("@idAluno", idAluno);
                return dao.queryListaTarefa(command);
            } catch(Exception) {
                return null;
            }
        }

        public List<DiarioDeNota> findAllByAlunoId(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT DiariosDeNota.*, Tarefas.TipoDaTarefa FROM (Alunos INNER JOIN DiariosDeNota ON Alunos.Id = DiariosDeNota.IdAluno) 
                                        INNER JOIN Tarefas ON (Tarefas.Id = DiariosDeNota.IdTarefa) AND (Alunos.Id = Tarefas.IdAluno)
                                        WHERE DiariosDeNota.IdAluno = @IdAluno";
                command.Parameters.AddWithValue("@IdAluno", id);
                return dao.queryListaNotas(command);
            } catch(Exception) {
                return null;
            }
        }

        public List<Tarefa> findAllByTipo(TipoTarefa tipo, int idAluno) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Tarefas WHERE TipoDaTarefa = @tipo";
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
                command.CommandText = @"INSERT INTO Tarefas (TipoDaTarefa, DataLimite, Descricao, IdAluno) VALUES (@tipo, @dataLimite, @descricao, @idAluno)";
                command.Parameters.AddWithValue("@tipo", tarefa.TipoDaTarefa.ToString());
                command.Parameters.AddWithValue("@dataLimite", tarefa.getDataFormatada());
                command.Parameters.AddWithValue("@descricao", tarefa.Descricao);
                command.Parameters.AddWithValue("@idAluno", tarefa.IdAluno);
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