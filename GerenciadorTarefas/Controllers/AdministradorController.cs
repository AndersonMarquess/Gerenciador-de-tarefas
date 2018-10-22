using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Models;
using System;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers
{
    public class AdministradorController : Controller
    {
        private IAdministradorDAO dao = new AdministradorDAO();

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entrar(string login, string senha) {
            Administrador admin = dao.findByCredenciais(login);

            if(admin != null) {
                bool isSenhaValida = BCrypt.Net.BCrypt.Verify(senha, admin.Senha);

                if(isSenhaValida) {
                    Session["usuarioLogado"] = admin;
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
        public ActionResult Cadastrar(Administrador admin) {
            var a = dao.findByCredenciais(admin.Login);

            if(a != null) {
                ModelState.AddModelError("erro-login", "Este login não está disponível.");
                return View("Form");
            }

            dao.insert(admin);
            return RedirectToAction("Index");
        }

        public ActionResult Logout() {
            Session["usuarioLogado"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult RecuperarSenha() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar(Administrador adminNovo) {
            Administrador admin = dao.findByCredenciais(adminNovo.Login);
            if(admin == null) {
                ModelState.AddModelError("erro-login", "Dados incorretos.");
                return View("RecuperarSenha");
            }

            var dadosInvalidos = !admin.Login.Equals(adminNovo.Login) 
                && !admin.PalavraBackup.Equals(adminNovo.PalavraBackup);

            if(dadosInvalidos) {
                ModelState.AddModelError("erro-login", "Dados incorretos.");
                return View("RecuperarSenha");
            }

            dao.updateSenha(adminNovo);
            return RedirectToAction("Index");
        }
    }
}