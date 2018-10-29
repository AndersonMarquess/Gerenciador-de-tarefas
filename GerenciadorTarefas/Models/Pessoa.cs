using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models
{
    public abstract class Pessoa {

        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "O Campo nome é obrigatório e deve conter entre 5 e 250 caracteres.")]
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