using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GerenciadorTarefas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //Redireciona qualquer exception para o contexto escolhido
        //protected void Application_Error(object sender, EventArgs e) {
        //    Exception exception = Server.GetLastError();
        //    Server.ClearError();
        //    Response.Redirect("/Tarefa/Index");
        //}
    }
}
