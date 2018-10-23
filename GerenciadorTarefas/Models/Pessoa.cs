﻿using System;

namespace GerenciadorTarefas.Models
{
    public abstract class Pessoa {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa() { }

        protected Pessoa(int id, string nome, string cpf, Endereco endereco, string sexo, DateTime dataNascimento) {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
            Sexo = sexo;
            DataNascimento = dataNascimento;
        }
    }
}