using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalFinance.Web.Controllers
{
    public class BillsController : Controller
    {
        private readonly IBillService _billService;

        public BillsController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _billService.GetBillsAsync());
        }
    }
}
