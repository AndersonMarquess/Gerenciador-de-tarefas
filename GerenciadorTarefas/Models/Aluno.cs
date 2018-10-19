namespace GerenciadorTarefas.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }

        private string _senha;
        public string Senha {
            get {
                return _senha;
            }
            set {
                var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                _senha = BCrypt.Net.BCrypt.HashPassword(value, salt);
            }
        }

        public Aluno() { }

        public Aluno(int id) { Id = id; }

        public Aluno(int id, string nome, string login, string senha) {
            Id = id;
            Nome = nome;
            Login = login;
            _senha = senha;
        }
    }
}