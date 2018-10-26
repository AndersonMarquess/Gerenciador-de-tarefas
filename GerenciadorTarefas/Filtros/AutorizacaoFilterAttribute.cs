using GerenciadorTarefas.Models;
using System;
using System.Web.Mvc;

namespace GerenciadorTarefas.Filtros
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var admin = filterContext.HttpContext.Session["usuarioLogado"] as Administrador;
            filterContext.Controller.ViewBag.DataHoje = DateTime.Today.ToString("dd/MM/yyyy");

            if(admin == null) {
                filterContext.Result = new RedirectResult("/Administrador/Index");
            } else {
                filterContext.Controller.ViewBag.Username = admin.Nome;
                filterContext.Controller.ViewBag.usuarioID = admin.Id;
            }
        }
    }
}