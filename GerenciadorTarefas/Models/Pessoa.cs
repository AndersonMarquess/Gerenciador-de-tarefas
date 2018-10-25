using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models
{
    public abstract class Pessoa {
        public int Id { get; set; }
        //[Required, Range(10, 250, ErrorMessage = "O Campo {0} é obrigatório e deve conter entre {1} e {2} caracteres.")]
        [Required]
        [MaxLength(250, ErrorMessage = "O Campo {0} é obrigatório e deve conter no máximo {1}.")]
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa() { }

        public Pessoa(int id, string nome, string cpf, Endereco endereco, string sexo, DateTime dataNascimento) {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
            Sexo = sexo;
            DataNascimento = dataNascimento;
        }
    }
}