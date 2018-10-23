using GerenciadorTarefas.DAO;
using GerenciadorTarefas.Models;
using System.Web.Mvc;

namespace GerenciadorTarefas.Controllers
{
    public class AlunoController : Controller
    {
        IAlunoDAO dao = new AlunoDAO();

        // GET: Aluno
        public ActionResult Index() {
            return View();
        }

        public ActionResult FormNovoAluno() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Aluno aluno) {
            //Verificar aluno
            dao.insert(aluno);
            return RedirectToAction("Index", "Tarefa");
        }

        public ActionResult Listar() {
            ViewBag.Alunos = dao.findAll();
            return View();
        }
    }
}