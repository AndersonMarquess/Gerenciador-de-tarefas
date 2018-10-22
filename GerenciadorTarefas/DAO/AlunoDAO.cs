namespace GerenciadorTarefas.DAO
{
    public class AlunoDAO
    {

        /*
        SELECT *

        FROM Alunos AS a,

            Enderecos AS e,

            Cidades AS c,

            Estados AS est

        WHERE a.IdEndereco = e.Id

         AND e.IdCidade = c.Id

         AND c.IdEstado = est.Id;
        */

        //Resposta da query
        //Aluno.ID, Aluno.Nome, Aluno.Cpf, Aluno.IdEndereco, Aluno.Matricula, Aluno.Serie, Endereco.Id, Endereco.Logradouro, Endereco.Cep, Endereco.Numero, Endereco.Bairro, Endereco.IdCidade, Cidade.Id, Cidade.Nome, Cidade.IdEstado, Estado.Id, Estado.Nome
        
        //Resultado de interesse
        //A.Id, A.Nome, A.Cpf, A.Matricula, A.Serie, 
        //E.Id, E.Logradouro, E.Cep, E.Numero, E.Bairro,
        //C.Id, C.Nome
        //Est.Id, Est.Nome
    }
}