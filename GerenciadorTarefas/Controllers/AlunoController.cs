using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Models;
using System;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers
{
    public class AlunoController : Controller
    {
        private IAlunoDAO dao = new AlunoDAO();

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entrar(string login, string senha) {
            Aluno aluno = dao.findByCredenciais(login);

            if(aluno != null) {
                bool isSenhaValida = BCrypt.Net.BCrypt.Verify(senha, aluno.Senha);

                if(isSenhaValida) {
                    Session["usuarioLogado"] = aluno;
                    return RedirectToAction("Index", "Tarefa");

                }
            }

            ModelState.AddModelError("erro-login", "Login e senha não correspondem.");
            return View("Index");
        }

        public ActionResult Form() {
            ViewBag.DataHoje = DateTime.Today.ToString("dd/MM/yyyy");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Aluno aluno) {
            var a = dao.findByCredenciais(aluno.Login);

            if(a != null) {
                ModelState.AddModelError("erro-login", "Este login não está disponível.");
                return View("Form");
            }

            dao.insert(aluno);
            return RedirectToAction("Index");
        }

        public ActionResult Logout() {
            Session["usuarioLogado"] = null;
            return RedirectToAction("Index");
        }
    }
}