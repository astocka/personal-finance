using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Models;
using PersonalFinance.Web.Services.Interfaces;

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

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bill bill = await _billService.GetBillByIdAsync((int)id);

            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Id,Amount,Date,DueDate,PaidDate,IsPaid,Notes")]Bill bill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bill.Amount = (decimal)bill.Amount;
                    await _billService.UpdateBillAsync(bill);
                    return RedirectToAction("Index", "Bills");
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(bill);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 
            await _billService.DeleteBillAsync(id);
            return RedirectToAction("Index", "Bills");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            if (search == null)
            {
                return NotFound();
            }

            try
            {
                var expenses = await _billService.SearchBillsAsync(search);
                return View(expenses);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
