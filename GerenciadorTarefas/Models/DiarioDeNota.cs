using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models
{
    public class DiarioDeNota {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public int IdTarefa { get; set; }
        [Range(0, 10, ErrorMessage = "A Nota não pode ser menor que 0 e maior que 10.")]
        public double NotaRecebida { get; set; }
        public string Observacoes { get; set; }

        public DiarioDeNota() { }

        public DiarioDeNota(int idAluno, int idTarefa, double notaRecebida, string observacoes) {
            IdAluno = idAluno;
            IdTarefa = idTarefa;
            NotaRecebida = notaRecebida;
            Observacoes = observacoes;
        }
    }
}