using GerenciadorTarefas.Models;
using System;
using System.Web.Mvc;

namespace GerenciadorTarefas.Filtros
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            Aluno aluno = filterContext.HttpContext.Session["usuarioLogado"] as Aluno;

            if(aluno == null) {
                filterContext.Controller.ViewBag.DataHoje = DateTime.Today.ToString("dd/MM/yyyy");
                filterContext.Result = new RedirectResult("/Aluno/Index");
            } else {
                filterContext.Controller.ViewBag.DataHoje = DateTime.Today.ToString("dd/MM/yyyy");
                filterContext.Controller.ViewBag.Username = aluno.Nome;
                filterContext.Controller.ViewBag.usuarioID = aluno.Id;
            }
        }
    }
}