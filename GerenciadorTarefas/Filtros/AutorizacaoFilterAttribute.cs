using GerenciadorTarefas.Models;
using System;
using System.Web.Mvc;

namespace GerenciadorTarefas.Filtros
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            Administrador admin = filterContext.HttpContext.Session["usuarioLogado"] as Administrador;

            if(admin == null) {
                filterContext.Controller.ViewBag.DataHoje = DateTime.Today.ToString("dd/MM/yyyy");
                filterContext.Result = new RedirectResult("/Administrador/Index");
            } else {
                filterContext.Controller.ViewBag.DataHoje = DateTime.Today.ToString("dd/MM/yyyy");
                filterContext.Controller.ViewBag.Username = admin.Nome;
                filterContext.Controller.ViewBag.usuarioID = admin.Id;
            }
        }
    }
}