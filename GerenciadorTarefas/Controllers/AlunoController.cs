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
    }
}