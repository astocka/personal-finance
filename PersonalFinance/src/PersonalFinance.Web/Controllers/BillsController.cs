using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Models;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Date,Notes,DueDate,IsPaid,PaidDate")]Bill bill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bill.Amount = (decimal)bill.Amount;
                    await _billService.CreateBillAsync(bill);
                    return RedirectToAction("Index","Bills");
                }
                catch(Exception)
                {
                    return NotFound();
                }

            }
            return View(bill);
        }
    }
}
