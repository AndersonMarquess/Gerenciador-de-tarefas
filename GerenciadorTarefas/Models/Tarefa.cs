using System;

namespace GerenciadorTarefas.Models
{
    public class Tarefa {

        public int Id { get; set; }
        public TipoTarefa TipoDaTarefa { get; set; }
        public DateTime DataLimite { get; set; }
        public string Descricao { get; set; }
        public int IdAluno { get; set; }

        public Tarefa() { }

        public Tarefa(int id, string tipoDaTarefa, DateTime dataLimite, string descricao, int idAluno) {
            try {
                Id = id;
                TipoDaTarefa = (TipoTarefa)Enum.Parse(typeof(TipoTarefa), tipoDaTarefa);
                DataLimite = dataLimite;
                Descricao = descricao;
                IdAluno = IdAluno;
            } catch (Exception e) {
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