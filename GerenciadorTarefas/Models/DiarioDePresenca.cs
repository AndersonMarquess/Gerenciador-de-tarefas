using System;

namespace GerenciadorTarefas.Models
{
    public class DiarioDePresenca {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public DateTime DataDaFalta { get; set; }

        public override bool Equals(object obj) {
            var presenca = obj as DiarioDePresenca;
            return presenca != null &&
                   IdAluno == presenca.IdAluno &&
                   DataDaFalta == presenca.DataDaFalta;
        }

        public string getDataDaFaltaFormatada() {
            return DataDaFalta.ToString("dd/MM/yyyy");
        }

        public override int GetHashCode() {
            var hashCode = 1497196669;
            hashCode = hashCode * -1521134295 + IdAluno.GetHashCode();
            hashCode = hashCode * -1521134295 + DataDaFalta.GetHashCode();
            return hashCode;
        }
    }
}