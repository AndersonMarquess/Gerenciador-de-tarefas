
using System;

namespace GerenciadorTarefas.Models {
    public class Administrador {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }

        private string _senha;
        public string Senha {
            get {
                return _senha;
            }
            set {
                _senha = HashString(value);
            }
        }

        private string _palavraBackup;
        public string PalavraBackup {
            get {
                return _palavraBackup;
            }
            set {
                _palavraBackup = HashString(value);
            }
        }

        public Administrador() { }

        public Administrador(string nome, string login, string senha, string palavraBackup) {
            Nome = nome;
            Login = login;
            Senha = senha;
            PalavraBackup = palavraBackup;
        }

        private string HashString(string value) {
            if(HasBCryptPrefix(value)) {
                return value;
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(value, salt);
        }

        private bool HasBCryptPrefix(string value) {
            var bcryptPrefix = "$2a$12";
            return value.StartsWith(bcryptPrefix);
        }
    }
}