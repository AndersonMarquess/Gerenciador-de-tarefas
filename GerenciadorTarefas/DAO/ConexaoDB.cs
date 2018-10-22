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
            //string dataSource = @"Data Source=C:\Users\Anderson\Desktop\GerenciadorDB.mdb";
            //string dataSource = @"Data Source=C:\Users\Anderson\TarefasDB.mdb";
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
                    int idEndereco =(int) reader[3];
                    int matricula = (int)reader[4];
                    string serie = reader[5].ToString();

                    //Query endereco
                    Endereco endereco = new Endereco();

                    aluno = new Aluno(id, nome, cpf, endereco, matricula, serie);
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
    }
}