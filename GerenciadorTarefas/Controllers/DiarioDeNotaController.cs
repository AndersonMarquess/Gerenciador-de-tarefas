using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Filtros;
using GerenciadorTarefas.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers
{
    [AutorizacaoFilter]
    public class DiarioDeNotaController : Controller
    {
        private IAlunoDAO alunoDAO = new AlunoDAO();
        private IDiarioDeNotaDAO dao = new DiarioDeNotaDAO();
        private ITarefaDAO tarefaDAO = new TarefaDAO();

        public ActionResult Index() {
            ViewBag.Alunos = alunoDAO.findAll();
            ViewBag.AlunoNome = "";
            ViewBag.Tarefas = new List<Tarefa>();
            ViewBag.IdAluno = -1;

            return View();
        }

        public ActionResult BuscarTarefas(int id) {
            ViewBag.IdAluno = id;
            ViewBag.AlunoNome = alunoDAO.findById(id).Nome;
            ViewBag.Alunos = alunoDAO.findAll();
            ViewBag.Tarefas = dao.findTarefasNaoEntreguesDoAluno(id);

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtribuirNota(DiarioDeNota diario) {
            if(ModelState.IsValid) {
                dao.insert(diario);
                return RedirectToAction("Index");
            }
            return BuscarTarefas(diario.IdAluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverNota(int idAluno, int idNota) {
            dao.remover(idNota);
            return RedirectToAction("Informacoes", "Aluno", new { id = idAluno });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(DiarioDeNota diario) {
            if(ModelState.IsValid) {
                dao.update(diario);
            }

            return RedirectToAction("Informacoes", "Aluno", new { id = diario.IdAluno });
        }
    }
}