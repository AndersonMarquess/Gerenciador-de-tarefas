
namespace GerenciadorTarefas.Models
{
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
                _senha = StringSegura(value);
            }
        }

        private string _palavraBackup;
        public string PalavraBackup {
            get {
                return _palavraBackup;
            }
            set {
                _palavraBackup = StringSegura(value);
            }
        }

        public Administrador() { }

        public Administrador(string nome, string login, string senha, string palavraBackup) {
            Nome = nome;
            Login = login;
            _senha = senha;
            _palavraBackup = palavraBackup;
        }

        private string StringSegura(string value) {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(value, salt);
        }
    }
}