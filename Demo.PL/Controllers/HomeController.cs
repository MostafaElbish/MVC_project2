using Demo.PL.Models;
using Demo.PL.Srevises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedServises scopedServises1;
        private readonly IScopedServises scopedServises2;
        private readonly ITransientServises transientServises1;
        private readonly ITransientServises transientServises2;
        private readonly IselgentonServises iselgentonServises1;
        private readonly IselgentonServises iselgentonServises2;

        public HomeController(ILogger<HomeController> logger,
           IScopedServises scopedServises1,
            IScopedServises scopedServises2,
            ITransientServises transientServises1,
            ITransientServises transientServises2,
            IselgentonServises iselgentonServises1,
            IselgentonServises iselgentonServises2

            )
        {
            _logger = logger;
            this.scopedServises1 = scopedServises1;
            this.scopedServises2 = scopedServises2;
            this.transientServises1 = transientServises1;
            this.transientServises2 = transientServises2;
            this.iselgentonServises1 = iselgentonServises1;
            this.iselgentonServises2 = iselgentonServises2;
        }

        public IActionResult Index()
        {
            return View();
        }
        public string TestLiveTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scopedServises1 ::{scopedServises1.GetGuid()}");
            builder.Append($"scopedServises2 ::{scopedServises2.GetGuid()}");
            builder.Append($"transientServises1 ::{transientServises1.GetGuid()}");
            builder.Append($"transientServises2 ::{transientServises2.GetGuid()}");
            builder.Append($"iselgentonServises1 ::{iselgentonServises1.GetGuid()}");
            builder.Append($"iselgentonServises2 ::{iselgentonServises2.GetGuid()}");
       return builder.ToString();
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
