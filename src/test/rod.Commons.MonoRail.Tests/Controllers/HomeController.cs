using Castle.MonoRail.Framework;

namespace rod.Commons.MonoRail.Tests.Controllers
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
