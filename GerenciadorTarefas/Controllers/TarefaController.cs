using GerenciadorTarefas.Filtros;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.Services;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers {

    [AutorizacaoFilter]
    public class TarefaController : Controller {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService) {
            _tarefaService = tarefaService;
        }

        public ActionResult Index() {
            return View("Buscar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListarPorTipo(string tipoDaTarefa) {
            if(tipoDaTarefa == null) {
                return View("Buscar");
            }
            var tarefas = _tarefaService.BuscarTodasPorTipo(tipoDaTarefa);
            ViewBag.Tarefas = tarefas;

            return View("Listar");
        }

        public ActionResult Listar() {
            var admin = GetUsuarioLogado();
            ViewBag.Tarefas = _tarefaService.BuscarTodasPorIdAdmin(admin.Id);

            return View();
        }

        private Administrador GetUsuarioLogado() {
            return Session["usuarioLogado"] as Administrador;
        }

        public ActionResult Form() {
            ViewBag.Tarefa = new Tarefa();
            return View("Cadastrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Tarefa tarefa) {
            tarefa.IdAdmin = GetUsuarioLogado().Id;
            if(_tarefaService.Cadastrar(tarefa)) {
                return RedirectToAction("Listar");
            } else {
                @ViewBag.t = tarefa;
                ModelState.AddModelError("erro-data", "A Data limite deve ser maior ou igual a hoje.");
                return View("Cadastrar");
            }
        }

        public ActionResult Concluida(int id) {
            _tarefaService.Concluir(id);
            return RedirectToAction("Listar");
        }

        public ActionResult Editar(int id) {
            var tarefa = _tarefaService.BuscarPorId(id);
            if(tarefa == null) {
                return RedirectToAction("Listar");
            }

            ViewBag.t = tarefa;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar(Tarefa tarefa) {
            if(_tarefaService.Atualizar(tarefa)) {
                return RedirectToAction("Listar");
            } else {
                @ViewBag.t = tarefa;
                ModelState.AddModelError("erro-data", "A Data limite deve ser maior ou igual a hoje.");
                return View("Editar");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remover(int id) {
            _tarefaService.RemoverPorId(id);
            return RedirectToAction("Listar");
        }
    }
}