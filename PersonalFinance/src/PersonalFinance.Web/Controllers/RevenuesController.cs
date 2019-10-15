using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Models;
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

        [HttpGet]
        public async Task<IActionResult> ImportRevenues()
        {
            var result = await _revenueService.ImportRevenueAsync();
            if (!result)
            {
                TempData["RevenuesExist"] = "Revenues already exist in database!";
            }
            return RedirectToAction("Index", "Revenues");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _revenueService.DeleteRevenueAsync(id);
            return RedirectToAction("Index", "Revenues");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Amount,Description,Payer,Notes")]Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    revenue.Amount = (decimal)revenue.Amount;
                    await _revenueService.CreateRevenueAsync(revenue);
                    return RedirectToAction("Index", "Revenues");
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(revenue);
        }
    }
}
