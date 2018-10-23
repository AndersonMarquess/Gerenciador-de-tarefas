using System;
using System.Collections.Generic;

namespace GerenciadorTarefas.Models
{
    public class Aluno: Pessoa
    {
        public int Matricula { get; set; }
        public string Serie { get; set; }

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