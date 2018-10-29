using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;

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

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "GestaoTarefas.mdb");

            string dataSource = @"Data Source=" + filePath;

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
                        Id = (int)reader[0],
                        IdAluno = (int)reader[1],
                        IdTarefa = (int)reader[2],
                        NotaRecebida = Convert.ToDouble(reader[3].ToString()),
                        Observacoes = reader[4].ToString()
                    };
                    notas.Add(n);
                } catch(Exception) { }
            }

            fecharConexao();
            return notas;
        }

        //internal List<DiarioDeNota> querListaDiarioDeNota(OleDbCommand command) {
        //    abrirConexao();
        //    var notas = new List<DiarioDeNota>();

        //    command.Connection = _conn;
        //    OleDbDataReader reader = command.ExecuteReader();

        //    while(reader.Read()) {
        //        var nota = new DiarioDeNota() {
        //            Id = (int)reader[0];
        //        };
        //        notas.Add(nota);
        //    }


        //    fecharConexao();
        //    return notas;
        //}

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
                    //3 = IdEndereco
                    int matricula = (int)reader[4];
                    string serie = reader[5].ToString();
                    string sexo = reader[6].ToString();
                    DateTime dataNascimento = DateTime.Parse(reader[7].ToString());
                    string telefone = reader[8].ToString();
                    string email = reader[9].ToString();

                    //Endereço
                    var endereco = new Endereco() {
                        Id = (int)reader[10],
                        Logradouro = reader[11].ToString(),
                        Cep = reader[12].ToString(),
                        Numero = reader[13].ToString(),
                        Bairro = reader[14].ToString(),
                        //15 = IdCidade, 16 = Cidades.Id
                        Cidade = reader[17].ToString(),
                        //18 = IdEstado, 19 = Estados.Id
                        Estado = reader[20].ToString()
                    };

                    aluno = new Aluno(id, nome, cpf, endereco, matricula, serie, sexo, dataNascimento);
                    aluno.Telefone = telefone;
                    aluno.Email = email;

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
                    admin.Id = id;
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
                    //3 - idEndereco
                    int matricula = (int)reader[4];
                    string serie = reader[5].ToString();
                    string sexo = reader[6].ToString();
                    DateTime dataNascimento = DateTime.Parse(reader[7].ToString());
                    string telefone = reader[8].ToString();
                    string email = reader[9].ToString();

                    var a = new Aluno(id, nome, cpf, new Endereco(), matricula, serie, sexo, dataNascimento);
                    a.Telefone = telefone;
                    a.Email = email;

                    alunos.Add(a);

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