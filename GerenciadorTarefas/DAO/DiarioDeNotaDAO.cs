using System;
using System.Collections.Generic;
using System.Data.OleDb;
using GerenciadorTarefas.Models;

namespace GerenciadorTarefas.DAO
{
    public class DiarioDeNotaDAO : IDiarioDeNotaDAO
    {
        private ConexaoDB dao = ConexaoDB.getInstance();

        public List<Tarefa> findTarefasNaoEntreguesDoAluno(int id) {
            var tarefas = new List<Tarefa>();
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Tarefas WHERE Tarefas.Id 
                                        NOT IN (SELECT IdTarefa FROM DiariosDeNota WHERE DiariosDeNota.IdAluno = @IdAluno)";
                command.Parameters.AddWithValue("@IdAluno", id);
                tarefas = dao.queryListaTarefa(command);
            } catch(Exception) { }
            return tarefas;
        }

        public void insert(DiarioDeNota diario) {
            try {

                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO DiariosDeNota (IdAluno, IdTarefa, NotaRecebida, Observacoes) 
                                        VALUES (@IdAluno, @IdTarefa, @NotaRecebida, @Observacoes)";
                command.Parameters.AddWithValue("@IdAluno", diario.IdAluno);
                command.Parameters.AddWithValue("@IdTarefa", diario.IdTarefa);
                command.Parameters.AddWithValue("@NotaRecebida", diario.NotaRecebida);
                command.Parameters.AddWithValue("@Observacoes", diario.Observacoes);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public List<DiarioDeNota> findTarefasEntreguesByAlunoId(int id) {
            var notas = new List<DiarioDeNota>();
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM DiariosDeNota WHERE IdAluno = @IdAluno";
                command.Parameters.AddWithValue("@IdAluno", id);
                notas = dao.queryListaNotas(command);
                return notas;
            } catch(Exception) { }
            return notas;
        }

        public void update(DiarioDeNota diario) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"UPDATE DiariosDeNota SET NotaRecebida = @NotaRecebida, Observacoes = @Observacoes 
                                        WHERE DiariosDeNota.Id = @IdDiario";
                command.Parameters.AddWithValue("@NotaRecebida", diario.NotaRecebida);
                command.Parameters.AddWithValue("@Observacoes", diario.Observacoes);
                command.Parameters.AddWithValue("@IdDiario", diario.Id);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }
    }
}