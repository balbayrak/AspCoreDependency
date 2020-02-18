using AspCoreDependency.Core.Concrete;
using AspCoreDependency.Test.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreDependency.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceA _serviceA;
        private readonly IServiceB _serviceB;
        private readonly IServiceC _serviceC;


        public HomeController(IServiceA serviceA, IServiceB serviceB, IServiceC serviceC)
        {
            _serviceA = serviceA;
            _serviceB = serviceB;
            _serviceC = serviceC;
        }

        public IActionResult Index()
        {
            ViewBag.ServiceA = _serviceA.Run();
            ViewBag.ServiceB = _serviceB.Run();
            ViewBag.ServiceC = _serviceC.Run();

            return View();
        }

        public IActionResult Index2()
        {
            IServiceA serviceA = DependencyResolver.Current.GetService<IServiceA>();
            IServiceB serviceB = DependencyResolver.Current.GetService<IServiceB>();
            IServiceC serviceC = DependencyResolver.Current.GetService<IServiceC>();

            ViewBag.ServiceA = serviceA.Run();
            ViewBag.ServiceB = serviceB.Run();
            ViewBag.ServiceC = serviceC.Run();

            return View("Index");
        }

        public IActionResult Index3()
        {
            IService serviceA = DependencyResolver.Current.GetServiceByName<IService>("serviceA");
            IService serviceB = DependencyResolver.Current.GetServiceByName<IService>("serviceB");
            IService serviceC = DependencyResolver.Current.GetServiceByName<IService>("serviceC");

            ViewBag.ServiceA = serviceA.Run();
            ViewBag.ServiceB = serviceB.Run();
            ViewBag.ServiceC = serviceC.Run();

            return View("Index");
        }
    }
}
