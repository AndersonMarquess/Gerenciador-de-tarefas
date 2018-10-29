using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models
{
    public class Endereco {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        [StringLength(9, MinimumLength = 8, ErrorMessage = "O Campo cep deve conter 8 caracteres.")]
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}