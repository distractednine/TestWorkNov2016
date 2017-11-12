using System.Web.Mvc;

namespace TestWorkNov2016.Controllers
{
    public class TemplatesController : Controller
    {
        public ViewResult Page(string viewName)
        {
            return View(viewName);
        }
    }
}