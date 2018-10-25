﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models
{
    public class Aluno: Pessoa
    {
        [Required]
        //[MaxLength(20, ErrorMessage = "O Campo {0} é obrigatório e deve conter no máximo {1}.")]
        public int Matricula { get; set; }
        public string Serie { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Aluno() { }

        public Aluno(int id, string nome, string cpf, Endereco endereco, int matricula, string serie, string sexo, DateTime dataNascimento): 
            base(id, nome, cpf, endereco, sexo, dataNascimento) {
            Matricula = matricula;
            Serie = serie;
        }

        public List<DiarioDePresenca> DiarioDePresenca { get; set; }
        public List<DiarioDeNota> DiarioDeNotas { get; set; }
    }
}