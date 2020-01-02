using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;
namespace GerenciadorTarefas.Services {
    public interface IAdministradorService {

        Administrador Entrar(string login, string senha);

        bool Cadastrar(Administrador administrador);

        bool AtualizarSenha(Administrador administrador);
    }
}
