using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Filtros;
using GerenciadorTarefas.Models;
using System;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers
{
    [AutorizacaoFilter]
    public class TarefaController : Controller
    {
        private ITarefaDAO dao = new TarefaDAO();

        public ActionResult Index() {
            return View("Buscar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListarPorTipo(string tipoDaTarefa) {
            var aluno = Session["usuarioLogado"] as Administrador;

            if(tipoDaTarefa == null)
                return View("Buscar");

            TipoTarefa tipo = (TipoTarefa)Enum.Parse(typeof(TipoTarefa), tipoDaTarefa);
            var tarefas = dao.findAllByTipo(tipo);
            ViewBag.Tarefas = tarefas;

            return View("Listar");
        }

        public ActionResult Listar() {
            var aluno = Session["usuarioLogado"] as Administrador;
            ViewBag.Tarefas = dao.findAll(aluno.Id);

            return View();
        }

        public ActionResult Form() {
            var admin = Session["usuarioLogado"] as Administrador;

            if(admin == null)
                return RedirectToAction("Index", "Admin");

            ViewBag.Tarefa = new Tarefa();
            return View("Cadastrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Tarefa tarefa) {
            ViewBag.Tarefa = tarefa;

            var view = validarTarefa(tarefa, "Cadastrar");
            if(view != null)
                return view;

            if(ModelState.IsValid) {
                dao.insert(tarefa);
                return RedirectToAction("Listar");
            }

            return View("Cadastrar");
        }

        public ActionResult Concluida(int id) {
            dao.concluir(id);
            return RedirectToAction("Listar");
        }

        public ActionResult Editar(int id) {
            var tarefa = dao.findById(id);
            if(tarefa == null)
                RedirectToAction("Listar");

            ViewBag.t = tarefa;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar(Tarefa tarefa) {
            var view = validarTarefa(tarefa, "Editar");
            if(view != null)
                return view;

            dao.update(tarefa);
            return RedirectToAction("Listar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remover(int id) {
            dao.delete(id);
            return RedirectToAction("Listar");
        }

        private ActionResult validarTarefa(Tarefa tarefa, string viewName) {
            if(tarefa.DataLimite < DateTime.Today) {
                @ViewBag.t = tarefa;
                ModelState.AddModelError("erro-data", "A Data limite deve ser maior ou igual a hoje.");
                return View(viewName);
            }
            return null;
        }
    }
}