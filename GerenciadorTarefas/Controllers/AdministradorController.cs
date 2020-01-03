using GerenciadorTarefas.Models;
using GerenciadorTarefas.Services;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers {
    public class AdministradorController : Controller {

        private readonly IAdministradorService _adminService;

        public AdministradorController(IAdministradorService adminService) {
            _adminService = adminService;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entrar(string login, string senha) {
            Administrador admin = _adminService.Entrar(login, senha);

            if(admin != null) {
                Session["usuarioLogado"] = admin;
                return RedirectToAction("Index", "Tarefa");
            }

            ModelState.AddModelError("erro-login", "Login e senha não correspondem.");
            return View("Index");
        }

        public ActionResult Form() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Administrador administrador) {
            var inserido = _adminService.Cadastrar(administrador);

            if(inserido) {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("erro-login", "Este login não está disponível.");
            return View("Form");
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
        public ActionResult Atualizar(Administrador administrador) {
            bool atualizou = _adminService.AtualizarSenha(administrador);
            if(atualizou) {
                return Logout();
            } else {
                ModelState.AddModelError("erro-login", "Dados incorretos.");
                return View("RecuperarSenha");
            }
        }
    }
}
