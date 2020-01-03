using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Filtros;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers {

    [AutorizacaoFilter]
    public class AlunoController : Controller {

        private readonly IAlunoService _alunoService;
        private readonly IDiarioDeNotaService _diarioDeNotaService;

        public AlunoController(IAlunoService alunoService, IDiarioDeNotaService diarioDeNotaService) {
            _alunoService = alunoService;
            _diarioDeNotaService = diarioDeNotaService;
        }

        public ActionResult Index() {
            ViewBag.Alunos = _alunoService.BuscarTodos();
            return View();
        }

        public ActionResult FormNovoAluno() {
            ViewBag.Aluno = new Aluno() {
                Endereco = new Endereco()
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Aluno aluno) {
            if(ModelState.IsValid) {
                _alunoService.Cadastrar(aluno);
                return RedirectToAction("Index", "Tarefa");
            }

            ViewBag.Aluno = aluno;
            return View("FormNovoAluno");
        }

        public ActionResult Listar() {
            ViewBag.Alunos = _alunoService.BuscarTodos();
            return View();
        }

        public ActionResult Informacoes(int id) {
            var aluno = _alunoService.BuscarPorId(id);
            if(aluno == null) {
                return RedirectToAction("Listar");
            }

            ViewBag.Faltas = _alunoService.BuscarFaltasDoAlunoComId(id);
            ViewBag.Notas = _diarioDeNotaService.BuscarDiariosDeTarefasEntregueDoAlunoComId(id);
            ViewBag.Aluno = aluno;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFalta(int id) {
            _alunoService.AdicionarFaltaAoAlunoComId(id);

            return RedirectToAction("Informacoes", "Aluno", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverFalta(int id, string dataFalta) {
            _alunoService.RemoverFaltaDoAlunoComId(id, dataFalta);

            return RedirectToAction("Informacoes", "Aluno", new { id });
        }

        public ActionResult Editar(int id) {
            ViewBag.Aluno = _alunoService.BuscarPorId(id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar(Aluno aluno) {
            if(ModelState.IsValid) {
                _alunoService.Atualizar(aluno);
                return RedirectToAction("Listar");
            }

            ViewBag.Aluno = aluno;
            return View("Editar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remover(int id) {
            _alunoService.Remover(id);
            return RedirectToAction("Listar");
        }
    }
}
