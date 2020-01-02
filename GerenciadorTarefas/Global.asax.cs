using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace GerenciadorTarefas {
    public class MvcApplication : System.Web.HttpApplication {

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegistrarComponentes();
        }

        /// <summary>
        /// Redireciona qualquer exception para o contexto escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected void Application_Error(object sender, EventArgs eventArgs) {
            Exception exception = Server.GetLastError();
            Console.WriteLine(exception.Message);
            Server.ClearError();
            Response.Redirect("/Aluno/Listar");
        }
    }
}
