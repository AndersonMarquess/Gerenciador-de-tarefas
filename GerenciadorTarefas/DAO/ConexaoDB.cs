using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace GerenciadorTarefas.DAO
{
    public class ConexaoDB
    {
        private OleDbConnection _conn;

        //Singleton
        private static ConexaoDB instance;
        private ConexaoDB() { }
        public static ConexaoDB getInstance() {
            if(instance == null)
                instance = new ConexaoDB();
            return instance;
        }

        private void abrirConexao() {
            try {
                _conn = new OleDbConnection(getConnPath());
                _conn.Open();
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void fecharConexao() {
            try {
                if(_conn != null) {
                    _conn.Close();
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private string getConnPath() {
            string provider = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            string dataSource = @"Data Source=C:\Users\Anderson\Desktop\GestaoTarefas.mdb";

            return provider + dataSource;
        }

        public void executarQuerySemRetorno(OleDbCommand command) {
            abrirConexao();

            command.Connection = _conn;
            command.ExecuteNonQuery();

            fecharConexao();
        }

        public object executarQueryComRetorno(OleDbCommand command) {
            abrirConexao();

            command.Connection = _conn;
            var result = command.ExecuteNonQuery();

            fecharConexao();
            return result;
        }

        internal List<DiarioDeNota> queryListaNotas(OleDbCommand command) {
            abrirConexao();

            var notas = new List<DiarioDeNota>();
            command.Connection = _conn;

            OleDbDataReader reader = command.ExecuteReader();

            while(reader.Read()) {
                try {
                    var n = new DiarioDeNota() {
                        IdAluno = (int)reader[1],
                        IdTarefa = (int)reader[2],
                        NotaRecebida = (double)reader[3],
                        Observacoes = reader[5].ToString()
                    };
                    notas.Add(n);
                } catch(Exception) { }
            }

            fecharConexao();
            return notas;
        }

        internal int queryForId(OleDbCommand command) {
            abrirConexao();
            int id = -1;

            command.Connection = _conn;
            OleDbDataReader reader = command.ExecuteReader();
            while(reader.Read()) {
                try {
                    id = Convert.ToInt32(reader[0].ToString());
                } catch(Exception) { }
            }

            fecharConexao();
            return id;
        }

        public List<Tarefa> queryListaTarefa(OleDbCommand command) {
            abrirConexao();
            var tarefas = new List<Tarefa>();

            command.Connection = _conn;
            OleDbDataReader reader = command.ExecuteReader();

            while(reader.Read()) {
                try {
                    int id = Convert.ToInt32(reader[0].ToString());
                    var tipo = reader[1].ToString();
                    DateTime dataLimite = DateTime.Parse(reader[2].ToString());
                    string desc = reader[3].ToString();
                    int idAluno = Convert.ToInt32(reader[4].ToString());

                    Tarefa t = new Tarefa(id, tipo, dataLimite, desc, idAluno);
                    tarefas.Add(t);
                } catch(Exception) { }
            }

            fecharConexao();
            return tarefas;
        }

        public Aluno queryAluno(OleDbCommand command) {
            abrirConexao();

            Aluno aluno = null;
            command.Connection = _conn;

            OleDbDataReader reader = command.ExecuteReader();

            while(reader.Read()) {
                try {
                    int id = (int)reader[0];
                    string nome = reader[1].ToString();
                    string cpf = reader[2].ToString();
                    int idEndereco = (int)reader[3];
                    int matricula = (int)reader[4];
                    string serie = reader[5].ToString();
                    string sexo = reader[6].ToString();
                    DateTime dataNascimento = DateTime.Parse(reader[7].ToString());

                    //Endereco
                    var endereco = new Endereco() {
                        Id = (int)reader[8],
                        Logradouro = reader[9].ToString(),
                        Cep = reader[10].ToString(),
                        Numero = reader[11].ToString(),
                        Bairro = reader[12].ToString(),
                        //13 = IdCidade, 14 = Cidades.Id
                        Cidade = reader[15].ToString(),
                        //16 = IdEstado, 17 = Estados.Id
                        Estado = reader[18].ToString()
                    };


                    aluno = new Aluno(id, nome, cpf, endereco, matricula, serie, sexo, dataNascimento);
                } catch(Exception) { }
            }

            fecharConexao();
            return aluno;
        }

        public Administrador queryAdministrador(OleDbCommand command) {
            abrirConexao();

            Administrador admin = null;
            command.Connection = _conn;

            OleDbDataReader reader = command.ExecuteReader();

            while(reader.Read()) {
                try {
                    int id = (int)reader[0];
                    string nome = reader[1].ToString();
                    string login = reader[2].ToString();
                    string senha = reader[3].ToString();
                    string palavraBackup = reader[4].ToString();

                    admin = new Administrador(nome, login, senha, palavraBackup);
                } catch(Exception) { }
            }

            fecharConexao();
            return admin;
        }

        public List<Aluno> queryListaAlunoSimples(OleDbCommand command) {
            abrirConexao();

            List<Aluno> alunos = new List<Aluno>();
            command.Connection = _conn;

            OleDbDataReader reader = command.ExecuteReader();

            while(reader.Read()) {
                try {
                    int id = (int)reader[0];
                    string nome = reader[1].ToString();
                    string cpf = reader[2].ToString();
                    int idEndereco = (int)reader[3];
                    int matricula = (int)reader[4];
                    string serie = reader[5].ToString();
                    string sexo = reader[6].ToString();
                    DateTime dataNascimento = DateTime.Parse(reader[7].ToString());

                    alunos.Add(new Aluno(id, nome, cpf, new Endereco(), matricula, serie, sexo, dataNascimento));
                } catch(Exception) { }
            }

            fecharConexao();
            return alunos;
        }

        public HashSet<DiarioDePresenca> queryListaPresenca(OleDbCommand command) {
            abrirConexao();

            var presencas = new HashSet<DiarioDePresenca>();
            command.Connection = _conn;

            OleDbDataReader reader = command.ExecuteReader();

            while(reader.Read()) {
                try {

                    var d = new DiarioDePresenca() {
                        IdAluno = (int)reader[0],
                        DataDaFalta = DateTime.Parse(reader[1].ToString())
                    };

                    presencas.Add(d);
                } catch(Exception) { }
            }

            fecharConexao();
            return presencas;
        }
    }
}