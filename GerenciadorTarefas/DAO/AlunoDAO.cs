using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace GerenciadorTarefas.DAO
{
    public class AlunoDAO : IAlunoDAO
    {
        ConexaoDB dao = ConexaoDB.getInstance();

        public void insert(Aluno aluno) {
            int idEndereco = inserirEnderecoCompleto(aluno.Endereco);
            if(idEndereco == -1)
                return;

            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Alunos (Nome, Cpf, IdEndereco, Matricula, Serie, Sexo, DataNascimento, Telefone, Email) 
                                        VALUES (@Nome, @Cpf, @IdEndereco, @Matricula, @Serie, @Sexo, @DataNascimento, @Telefone, @Email)";

                command.Parameters.AddWithValue("@Nome", aluno.Nome);
                command.Parameters.AddWithValue("@Cpf", aluno.Cpf);
                command.Parameters.AddWithValue("@IdEndereco", idEndereco);
                command.Parameters.AddWithValue("@Matricula", aluno.Matricula);
                command.Parameters.AddWithValue("@Serie", aluno.Serie);
                command.Parameters.AddWithValue("@Sexo", aluno.Sexo);
                command.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
                command.Parameters.AddWithValue("@Telefone", aluno.Telefone);
                command.Parameters.AddWithValue("@Email", aluno.Email);

                dao.executarQuerySemRetorno(command);

            } catch(Exception) { }
        }

        public void delete(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"DELETE FROM Alunos WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                dao.executarQuerySemRetorno(command);

            } catch(Exception) { }
        }

        public void update(Aluno aluno) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"UPDATE Alunos SET Nome = @Nome, Cpf = @Cpf, Matricula = @Matricula, Serie = @Serie, 
                                        Sexo = @Sexo, DataNascimento = @DataNascimento, Telefone = @Telefone, Email = @Email 
                                        WHERE Id = @IdAluno";

                command.Parameters.AddWithValue("@Nome", aluno.Nome);
                command.Parameters.AddWithValue("@Cpf", aluno.Cpf);
                command.Parameters.AddWithValue("@Matricula", aluno.Matricula);
                command.Parameters.AddWithValue("@Serie", aluno.Serie);
                command.Parameters.AddWithValue("@Sexo", aluno.Sexo);
                command.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
                command.Parameters.AddWithValue("@Telefone", aluno.Telefone);
                command.Parameters.AddWithValue("@Email", aluno.Email);
                command.Parameters.AddWithValue("@IdAluno", aluno.Id);
                dao.executarQuerySemRetorno(command);

                UpdateEndereco(aluno);
            } catch(Exception) { }
        }

        private void UpdateEndereco(Aluno aluno) {
            try {
                var idEstado = inserirEstado(aluno.Endereco);
                var idCidade = updateCidade(aluno.Endereco, idEstado);
                
                //Endereco
                var commandUpdateEndereco = new OleDbCommand();
                commandUpdateEndereco.CommandText = @"UPDATE Enderecos SET Logradouro = @Logradouro, Cep = @Cep, Numero = @Numero, 
                                        Bairro = @Bairro, IdCidade = @IdCidade WHERE Id = @IdEndereco";

                commandUpdateEndereco.Parameters.AddWithValue("@Logradouro", aluno.Endereco.Logradouro);
                commandUpdateEndereco.Parameters.AddWithValue("@Cep", aluno.Endereco.Cep);
                commandUpdateEndereco.Parameters.AddWithValue("@Numero", aluno.Endereco.Numero);
                commandUpdateEndereco.Parameters.AddWithValue("@Bairro", aluno.Endereco.Bairro);
                commandUpdateEndereco.Parameters.AddWithValue("@IdCidade", idCidade);
                commandUpdateEndereco.Parameters.AddWithValue("@IdEndereco", aluno.Endereco.Id);
                dao.executarQuerySemRetorno(commandUpdateEndereco);
            } catch(Exception) { }
        }

        public List<Aluno> findAll() {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Alunos";
                return dao.queryListaAlunoSimples(command);
            } catch(Exception) { }

            return null;
        }

        public Aluno findById(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT Alunos.*, Enderecos.*, Cidades.*, Estados.* FROM Estados 
                                        INNER JOIN ((Cidades INNER JOIN Enderecos ON Cidades.Id = Enderecos.IdCidade) 
                                        INNER JOIN Alunos ON Enderecos.Id = Alunos.IdEndereco) 
                                        ON Estados.Id = Cidades.IdEstado WHERE Alunos.Id = @IdAluno";

                command.Parameters.AddWithValue("@IdAluno", id);
                return dao.queryAluno(command);

            } catch(Exception) { }

            return null;
        }

        public HashSet<DiarioDePresenca> findAllFaltasByAlunoId(int id) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT IdAluno, DataFalta FROM DiariosDePresenca AS ddp WHERE ddp.IdAluno = @IdAluno";
                command.Parameters.AddWithValue("@IdAluno", id);
                return dao.queryListaPresenca(command);
            } catch(Exception) { }
            return null;
        }

        public void addFaltaAluno(DiarioDePresenca diario) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO DiariosDePresenca (IdAluno, DataFalta) VALUES (@IdAluno, @DataFalta)";
                command.Parameters.AddWithValue("@IdAluno", diario.IdAluno);
                command.Parameters.AddWithValue("@DataFalta", diario.DataDaFalta);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        public void removerFaltaByAlunoId(int id, DateTime data) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"DELETE FROM DiariosDePresenca WHERE IdAluno = @IdAluno AND DataFalta = @DataFalta";
                command.Parameters.AddWithValue("@IdAluno", id);
                command.Parameters.AddWithValue("@DataFalta", data);
                dao.executarQuerySemRetorno(command);
            } catch(Exception) { }
        }

        private int inserirEnderecoCompleto(Endereco endereco) {
            try {
                //Estado
                int idEstado = inserirEstado(endereco);

                //Cidade
                int idCidade = inserirCidade(endereco, idEstado);

                //Endereco
                var commandEnderecoInsert = new OleDbCommand();
                commandEnderecoInsert.CommandText = @"INSERT INTO Enderecos (Logradouro, Cep, Numero, Bairro, IdCidade)
                                        VALUES (@Logradouro, @Cep, @Numero, @Bairro, @IdCidade)";

                commandEnderecoInsert.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
                commandEnderecoInsert.Parameters.AddWithValue("@Cep", endereco.Cep);
                commandEnderecoInsert.Parameters.AddWithValue("@Numero", endereco.Numero);
                commandEnderecoInsert.Parameters.AddWithValue("@Bairro", endereco.Bairro);
                commandEnderecoInsert.Parameters.AddWithValue("@IdCidade", idCidade);
                dao.executarQuerySemRetorno(commandEnderecoInsert);

                return findEnderecoId(endereco);
            } catch(Exception) { }
            return -1;
        }

        private int inserirCidade(Endereco endereco, int idEstado) {
            var idCidade = findCidadeId(endereco);
            if(idCidade >= 0) {
                return idCidade;
            }

            var commandCidadeInsert = new OleDbCommand();
            commandCidadeInsert.CommandText = @"INSERT INTO Cidades (Nome, IdEstado) VALUES (@Nome, @IdEstado)";
            commandCidadeInsert.Parameters.AddWithValue("@Nome", endereco.Cidade);
            commandCidadeInsert.Parameters.AddWithValue("@IdEstado", idEstado);
            dao.executarQuerySemRetorno(commandCidadeInsert);

            return idCidade = findCidadeId(endereco);
        }

        private int updateCidade(Endereco endereco, int idEstado) {
            var idCidade = findCidadeId(endereco);
            if(idCidade >= 0) {
                var commandCidadeUpdate = new OleDbCommand();
                commandCidadeUpdate.CommandText = @"UPDATE Cidades SET Nome = @Nome, IdEstado = @IdEstado WHERE Id = @IdCidade";
                commandCidadeUpdate.Parameters.AddWithValue("@Nome", endereco.Cidade);
                commandCidadeUpdate.Parameters.AddWithValue("@IdEstado", idEstado);
                commandCidadeUpdate.Parameters.AddWithValue("@IdCidade", idCidade);
                dao.executarQuerySemRetorno(commandCidadeUpdate);
                return idCidade = findCidadeId(endereco);
            }

            return inserirCidade(endereco, idEstado);
        }

        private int inserirEstado(Endereco endereco) {
            var idEstado = findEstadoId(endereco);
            if(idEstado >= 0) {
                return idEstado;
            }

            var commandEstadoInsert = new OleDbCommand();
            commandEstadoInsert.CommandText = @"INSERT INTO Estados (Nome) VALUES (@Nome)";
            commandEstadoInsert.Parameters.AddWithValue("@Nome", endereco.Estado);
            dao.executarQuerySemRetorno(commandEstadoInsert);

            return idEstado = findEstadoId(endereco);
        }

        private int findEnderecoId(Endereco endereco) {
            var commandEnderecoSelect = new OleDbCommand();
            commandEnderecoSelect.CommandText = @"SELECT * FROM Enderecos WHERE Logradouro = @Logradouro AND Cep = @Cep AND Numero = @Numero";
            commandEnderecoSelect.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
            commandEnderecoSelect.Parameters.AddWithValue("@Cep", endereco.Cep);
            commandEnderecoSelect.Parameters.AddWithValue("@Numero", endereco.Numero);

            return dao.queryForId(commandEnderecoSelect);
        }

        private int findCidadeId(Endereco endereco) {
            var commandCidadeSelect = new OleDbCommand();
            commandCidadeSelect.CommandText = @"SELECT * FROM Cidades WHERE Nome = @Nome";
            commandCidadeSelect.Parameters.AddWithValue("@Nome", endereco.Cidade);
            var idCidade = dao.queryForId(commandCidadeSelect);
            return idCidade;
        }

        private int findEstadoId(Endereco endereco) {
            var commandEstadoSelect = new OleDbCommand();
            commandEstadoSelect.CommandText = @"SELECT * FROM Estados WHERE Nome = @Nome";
            commandEstadoSelect.Parameters.AddWithValue("@Nome", endereco.Estado);
            var idEstado = dao.queryForId(commandEstadoSelect);
            return idEstado;
        }
    }
}