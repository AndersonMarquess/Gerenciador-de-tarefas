using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models {
    public class Tarefa {

        public int Id { get; set; }
        [Required]
        public TipoTarefa TipoDaTarefa { get; set; }
        public DateTime DataLimite { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "A Descrição é obrigatória deve conter entre 5 e 250 caracteres.")]
        public string Descricao { get; set; }
        public int IdAdmin { get; set; }
        public AndamentoTarefa Andamento { get; set; } = AndamentoTarefa.Em_Andamento;

        public Tarefa() { }

        public Tarefa(int id, string tipoDaTarefa, DateTime dataLimite, string descricao, int idAluno) {
            try {
                Id = id;
                TipoDaTarefa = (TipoTarefa)Enum.Parse(typeof(TipoTarefa), tipoDaTarefa);
                DataLimite = dataLimite;
                Descricao = descricao;
                IdAdmin = IdAdmin;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public string getDataFormatada() {
            return DataLimite.ToString("dd/MM/yyyy");
        }

        public string getDataFormatadaBrowser() {
            return DataLimite.ToString("yyyy-MM-dd");
        }
    }
}