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
            Aluno aluno = Session["usuarioLogado"] as Aluno;

            if(tipoDaTarefa == null)
                return View("Buscar");

            TipoTarefa tipo = (TipoTarefa)Enum.Parse(typeof(TipoTarefa), tipoDaTarefa);
            var tarefas = dao.findAllByTipo(tipo, aluno.Id);
            ViewBag.Tarefas = tarefas;

            return View("Listar");
        }

        public ActionResult Listar() {
            Aluno aluno = Session["usuarioLogado"] as Aluno;
            ViewBag.Tarefas = dao.findAll(aluno.Id);

            return View();
        }

        public ActionResult Form() {
            Aluno aluno = Session["usuarioLogado"] as Aluno;

            if(aluno == null)
                return RedirectToAction("Index", "Aluno");

            return View("Cadastrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Tarefa tarefa) {
            var view = validarTarefa(tarefa, "Cadastrar");
            if(view != null)
                return view;

            dao.insert(tarefa);
            return RedirectToAction("Listar");
        }

        public ActionResult Concluida(int id) {
            dao.delete(id);
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

            if(tarefa.Descricao == null || tarefa.Descricao.Length > 250) {
                @ViewBag.t = tarefa;
                ModelState.AddModelError("erro-descricao",
                    "A Descrição é obrigatória e deve conter no máximo 250 caracteres.");
                return View(viewName);
            }

            return null;
        }
    }
}