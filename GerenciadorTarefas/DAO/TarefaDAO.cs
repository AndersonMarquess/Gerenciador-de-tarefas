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

        public List<Tarefa> findAll(int idAdmin) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Tarefas WHERE IdAdmin = @idAdmin";
                command.Parameters.AddWithValue("@idAdmin", idAdmin);
                return dao.queryListaTarefa(command);
            } catch(Exception) {
                return null;
            }
        }

        public List<DiarioDeNota> findAllByAlunoId(int id) {
            try {
                var command = new OleDbCommand();
                //command.CommandText = @"SELECT DiariosDeNota.*, Tarefas.TipoDaTarefa FROM (Alunos INNER JOIN DiariosDeNota ON Alunos.Id = DiariosDeNota.IdAluno) 
                //                        INNER JOIN Tarefas ON (Tarefas.Id = DiariosDeNota.IdTarefa) AND (Alunos.Id = Tarefas.IdAluno)
                //                        WHERE DiariosDeNota.IdAluno = @IdAluno";
                //command.Parameters.AddWithValue("@IdAluno", id);

                //Fazer query que busca o id da tarefa, a data do recebimento da nota, a nota recebida, e o tipo de tarefa.
                return dao.queryListaNotas(command);
            } catch(Exception) {
                return null;
            }
        }

        public List<Tarefa> findAllByTipo(TipoTarefa tipo) {
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