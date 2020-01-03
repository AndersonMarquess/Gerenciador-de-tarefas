using GerenciadorTarefas.Filtros;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers {

    [AutorizacaoFilter]
    public class DiarioDeNotaController : Controller {

        private readonly IDiarioDeNotaService _diarioService;
        private readonly IAlunoService _alunoService;

        public DiarioDeNotaController(IDiarioDeNotaService diarioService, IAlunoService alunoService) {
            _diarioService = diarioService;
            _alunoService = alunoService;
        }

        public ActionResult Index() {
            ViewBag.Alunos = _alunoService.BuscarTodos();
            ViewBag.AlunoNome = "";
            ViewBag.Tarefas = new List<Tarefa>();
            ViewBag.IdAluno = -1;

            return View();
        }

        public ActionResult BuscarTarefas(int id) {
            ViewBag.IdAluno = id;
            ViewBag.AlunoNome = _alunoService.BuscarPorId(id).Nome;
            ViewBag.Alunos = _alunoService.BuscarTodos();
            ViewBag.Tarefas = _diarioService.BuscarTarefasNaoEntregueDoAlunoComId(id);

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtribuirNota(DiarioDeNota diario) {
            if(ModelState.IsValid) {
                _diarioService.Cadastrar(diario);
                return RedirectToAction("Index");
            }
            return BuscarTarefas(diario.IdAluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverNota(int idAluno, int idNota) {
            _diarioService.RemoverPorId(idNota);
            return RedirectToAction("Informacoes", "Aluno", new { id = idAluno });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(DiarioDeNota diario) {
            if(ModelState.IsValid) {
                _diarioService.Atualizar(diario);
            }

            return RedirectToAction("Informacoes", "Aluno", new { id = diario.IdAluno });
        }
    }
}