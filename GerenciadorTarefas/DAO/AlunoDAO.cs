using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace GerenciadorTarefas.DAO
{
    public class AlunoDAO : IAlunoDAO
    {
        ConexaoDB dao = ConexaoDB.getInstance();

        /*
            SELECT * FROM Alunos AS a, Enderecos AS e, Cidades AS c, Estados AS est WHERE a.IdEndereco = e.Id AND e.IdCidade = c.Id AND c.IdEstado = est.Id;
        */

        /*
            SELECT Alunos.*, Enderecos.*, Cidades.*, Estados.*
FROM Estados INNER JOIN ((Cidades INNER JOIN Enderecos ON Cidades.Id = Enderecos.IdCidade) INNER JOIN Alunos ON Enderecos.Id = Alunos.IdEndereco) ON Estados.Id = Cidades.IdEstado;
         */

        //Resposta da query 1ª query
        //Aluno.ID, Aluno.Nome, Aluno.Cpf, Aluno.IdEndereco, Aluno.Matricula, Aluno.Serie, Endereco.Id, Endereco.Logradouro, Endereco.Cep, Endereco.Numero, Endereco.Bairro, Endereco.IdCidade, Cidade.Id, Cidade.Nome, Cidade.IdEstado, Estado.Id, Estado.Nome

        //Resultado de interesse
        //A.Id, A.Nome, A.Cpf, A.Matricula, A.Serie, 
        //E.Id, E.Logradouro, E.Cep, E.Numero, E.Bairro,
        //C.Id, C.Nome
        //Est.Id, Est.Nome

        private int inserirEstadoERecuperarId(Endereco endereco) {
            int idEstado = recuperarIdEstado(endereco);
            if(idEstado >= 0)
                return idEstado;

            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Estados (Nome) VALUES (@Nome)";
                command.Parameters.AddWithValue("@Nome", endereco.Estado);
                dao.executarQuerySemRetorno(command);
                return recuperarIdEstado(endereco);
            } catch(Exception) { }
            return -1;
        }

        private int recuperarIdEstado(Endereco endereco) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Estados WHERE Nome = @Nome";
                command.Parameters.AddWithValue("@Nome", endereco.Estado);
                return dao.queryForId(command);
            } catch(Exception) { }
            return -1;
        }

        private int inserirCidadeERecuperarId(Endereco endereco) {
            int idCidade = recuperarIdCidade(endereco);
            if(idCidade >= 0)
                return idCidade;

            int idEstado = inserirEstadoERecuperarId(endereco);
            if(idEstado == -1)
                return -1;

            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Cidades (Nome, IdEstado) VALUES (@Nome, @IdEstado)";
                command.Parameters.AddWithValue("@Nome", endereco.Cidade);
                command.Parameters.AddWithValue("@IdEstado", idEstado);
                dao.executarQuerySemRetorno(command);

                return recuperarIdCidade(endereco);
            } catch(Exception) { }
            return -1;
        }

        private int recuperarIdCidade(Endereco endereco) {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Cidades WHERE Nome = @Nome";
                command.Parameters.AddWithValue("@Nome", endereco.Cidade);
                return dao.queryForId(command);
            } catch(Exception) { }
            return -1;
        }

        private int inserirEnderecoERecuperarId(Endereco endereco) {
            int idCidade = inserirCidadeERecuperarId(endereco);
            if(idCidade == -1)
                return -1;

            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Enderecos (Logradouro, Cep, Numero, Bairro, IdCidade)
                                        VALUES (@Logradouro, @Cep, @Numero, @Bairro, @IdCidade)";

                command.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
                command.Parameters.AddWithValue("@Cep", endereco.Cep);
                command.Parameters.AddWithValue("@Numero", endereco.Numero);
                command.Parameters.AddWithValue("@Bairro", endereco.Bairro);
                command.Parameters.AddWithValue("@IdCidade", idCidade);
                dao.executarQuerySemRetorno(command);

                return recuperarIdEndereco(endereco);
            } catch(Exception) { }
            return -1;
        }

        private int recuperarIdEndereco(Endereco endereco) {
            try {
                var command = new OleDbCommand();

                command.CommandText = @"SELECT * FROM Enderecos WHERE Logradouro = @Logradouro AND Cep = @Cep AND Numero = @Numero AND Bairro = @Bairro";
                command.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
                command.Parameters.AddWithValue("@Cep", endereco.Cep);
                command.Parameters.AddWithValue("@Numero", endereco.Numero);
                command.Parameters.AddWithValue("@Bairro", endereco.Bairro);
                return dao.queryForId(command);
            } catch(Exception) { }
            return -1;
        }

        //Consegue inserir o estado, inseri a cidade e não recupera seu id causando return nos outros métodos.
        public void insert(Aluno aluno) {
            int idEndereco = inserirEnderecoERecuperarId(aluno.Endereco);
            if(idEndereco == -1)
                return;

            try {
                var command = new OleDbCommand();
                command.CommandText = @"INSERT INTO Alunos (Nome, Cpf, IdEndereco, Matricula, Serie, Sexo, DataNascimento) 
                                        VALUES (@Nome, @Cpf, @IdEndereco, @Matricula, @Serie, @Sexo, @DataNascimento)";

                command.Parameters.AddWithValue("@Nome", aluno.Nome);
                command.Parameters.AddWithValue("@Cpf", aluno.Cpf);
                command.Parameters.AddWithValue("@IdEndereco", idEndereco);
                command.Parameters.AddWithValue("@Matricula", aluno.Matricula);
                command.Parameters.AddWithValue("@Serie", aluno.Serie);
                command.Parameters.AddWithValue("@Sexo", aluno.Sexo);
                command.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);

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
            throw new System.NotImplementedException();
        }

        public List<Aluno> findAll() {
            try {
                var command = new OleDbCommand();
                command.CommandText = @"SELECT * FROM Alunos";
                return dao.queryListaAlunoSimples(command);
            } catch(Exception) { }

            return null;
        }
    }
}