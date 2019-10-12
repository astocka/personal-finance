using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Controllers
{
    public class RevenuesController : Controller
    {
        private readonly IRevenueService _revenueService;

        public RevenuesController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _revenueService.GetRevenuesAsync());
        }
    }
}
