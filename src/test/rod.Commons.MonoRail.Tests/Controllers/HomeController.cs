using Castle.MonoRail.Framework;

namespace Rod.Commons.MonoRail.Tests.Controllers
{
    internal class HomeController : Controller
    {
        public void Index()
        {
        }

        public void Other()
        {
            RenderView("display");
        }
    }
}
