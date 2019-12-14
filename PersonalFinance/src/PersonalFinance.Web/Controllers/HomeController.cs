using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Helpers;
using PersonalFinance.Web.Services.Interfaces;

namespace PersonalFinance.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IBillService _billService;

        public HomeController(IBudgetService budgetService, IBillService billService)
        {
            _budgetService = budgetService;
            _billService = billService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentBudget = await _budgetService.GetCurrentBudget();
            ViewBag.UnpaidBills = await _billService.GetUnpaidBillsAsync();

            if (currentBudget != null)
            {
                ViewBag.Dashboard = "active";
                return View(currentBudget);
            }
            return RedirectToAction("Create", "Budget");
        }
    }
}
