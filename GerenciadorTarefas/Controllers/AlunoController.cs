using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Filtros;
using GerenciadorTarefas.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers
{
    [AutorizacaoFilter]
    public class AlunoController : Controller
    {
        IAlunoDAO dao = new AlunoDAO();
        //ITarefaDAO tarefaDAO = new TarefaDAO();

        // GET: Aluno
        public ActionResult Index() {
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
                dao.insert(aluno);
                return RedirectToAction("Index", "Tarefa");
            }

            ViewBag.Aluno = aluno;
            return View("FormNovoAluno");
        }

        public ActionResult Listar() {
            ViewBag.Alunos = dao.findAll();
            return View();
        }

        public ActionResult Informacoes(int id) {
            if(id < 0)
                return RedirectToAction("Listar");

            Aluno aluno = dao.findById(id);

            HashSet<DiarioDePresenca> faltas = dao.findAllFaltasByAlunoId(id);
            ViewBag.Faltas = faltas;

            //List<DiarioDeNota> notas = tarefaDAO.findAllByAlunoId(id);
            //ViewBag.Tarefas = notas;

            ViewBag.Aluno = aluno;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFalta(int id) {
            var d = new DiarioDePresenca() {
                IdAluno = id,
                DataDaFalta = DateTime.Parse(DateTime.Now.ToShortDateString())
            };
            dao.addFaltaAluno(d);

            return RedirectToAction("Informacoes", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverFalta(int id, string dataFalta) {
            var data = DateTime.Parse(dataFalta);
            dao.removerFaltaByAlunoId(id, data);

            return RedirectToAction("Informacoes", id);
        }

        public ActionResult Editar(int id) {
            var aluno = dao.findById(id);
            ViewBag.Aluno = aluno;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar(Aluno aluno) {
            if(ModelState.IsValid) {
                dao.update(aluno);
                return RedirectToAction("Listar");
            }

            ViewBag.Aluno = aluno;
            return View("Editar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remover(int id) {
            dao.delete(id);
            return RedirectToAction("Listar");
        }
    }
}