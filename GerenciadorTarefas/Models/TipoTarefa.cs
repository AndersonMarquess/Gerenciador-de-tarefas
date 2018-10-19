using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models
{
    public enum TipoTarefa
    {   
        [Display(Name="Atividade Complementar")]
        Atividade_Complementar,
        Trabalho,
        Prova,
        DP,
        Outro
    }
}