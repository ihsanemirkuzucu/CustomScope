using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScopeMiddlewareSample.Models;
using ScopeMiddlewareSample.Models.Entity;
using ScopeMiddlewareSample.Models.MyCustomScope;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ScopeMiddlewareSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LifeTimeEx _lifeTimeEx1;
        private readonly LifeTimeEx _lifeTimeEx2;
        private readonly MyScopeEntity _myScopeEntity;
        private readonly MyTransientEntity _myTransientEntity;
        private readonly IServiceProvider _serviceProvider;

        public HomeController(ILogger<HomeController> logger, LifeTimeEx lifeTimeEx, LifeTimeEx lifeTimeEx2, MyScopeEntity myScopeEntity, MyTransientEntity myTransientEntity, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _lifeTimeEx1 = lifeTimeEx;
            _lifeTimeEx2 = lifeTimeEx2;
            _myScopeEntity = myScopeEntity;
            _myTransientEntity = myTransientEntity;
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            string id1 = _lifeTimeEx1.GetGuid();
            string id2 = _lifeTimeEx2.GetGuid();
            string id3 = _myScopeEntity.GetTransientId();
            string id4 = _myScopeEntity.GetId();
            string id5 = _myTransientEntity.ID.ToString();

            var customLifeTime = new MyCustomLifeTime(HttpContext);
            using ( var scope = customLifeTime.CreateScope())
            {
                var myService1 = scope.ServiceProvider.GetRequiredService<MyCustomScopeEntity>();
                ViewData["id0"] = myService1.GetId();
            }

            ViewData["id1"] = id1;
            ViewData["id2"] = id2;
            ViewData["id3"] = id3;
            ViewData["id4"] = id4;
            ViewData["id5"] = id5;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
