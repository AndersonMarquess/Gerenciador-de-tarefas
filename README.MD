<h2 align="center">
	Sistema gerenciador de tarefas acadêmicas.
	<br>
	<a href="https://www.codefactor.io/repository/github/andersonmarquess/gerenciador-de-tarefas">
		<img src="https://www.codefactor.io/repository/github/andersonmarquess/gerenciador-de-tarefas/badge" align="center">
	</a>
	<br>
</h2>

Trabalho da faculdade (2018) com objetivo de construir um sistema para gerenciar tarefas acadêmicas com alerta em tela quando a data de envio de alguma atividade se aproximar.

## Histórico de versões 📅
- [v2](https://github.com/AndersonMarquess/Gerenciador-de-tarefas/tree/v2)
	- Persistência com MySQL, Integração com Entity Framework e adição da camada de serviço.
- [v1.1](https://github.com/AndersonMarquess/Gerenciador-de-tarefas/tree/v1.1)
	- Persistêcia com Access.

## Execução 💻

 - Rode o MySQL na porta padrão com user e password <i>root</i>
 	- Caso deseje alterar as credênciais de acesso ao banco, vá ao Web.config e mude a ConnectionString
 - Abra o projeto com [Visual Studio](https://visualstudio.microsoft.com/pt-br/)
 	- Acesse o console do Package Manager e digite: update-database
 - Inicie o projeto na IDE
	- Login padrão:
		- Login: admin
		- Senha: admin
		- Palavra de backup: admin
	- Crie sua conta:
		- localhost:{porta}/Administrador/Form

<p align="center">
    <img src="https://i.ibb.co/XYFXwV8/gerenciador-tarefas.gif" align="center" alt="imagem animada do sistema">
</p>

## Tecnologias usadas 🛠
- Back-end
	- ASP.NET MVC 5
	- Entity Framework
	- MySQL
- Front-end
	- Razor
	- HTML
	- CSS
	- Bootstrap
