using System.Web.Mvc;

namespace EventiWebAPI.Areas.Gestione
{
    public class GestioneAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Gestione";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Gestione_default",
                "Gestione/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}