using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Models;
using PersonalFinance.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _budgetService.GetBudgetAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Month,Year,TotalRevenue,PlannedExpenses")]Budget budget)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    budget.TotalRevenue = (decimal)budget.TotalRevenue;
                    budget.PlannedExpenses = (decimal)budget.PlannedExpenses;
                    budget.Balance = budget.TotalRevenue - budget.PlannedExpenses;
                    budget.Revenues = new List<PlannedRevenue>
                    {
                        new PlannedRevenue()
                        {
                            Amount = 10000,
                            Kind = RevenueKind.Salary,
                            Date = DateTime.Parse("2019-11-07")
                        }
                    };
                    budget.Expenses = new List<PlannedExpense>
                    {
                        new PlannedExpense()
                        {
                            Amount = 1000,
                            Kind = ExpenseKind.Rent,
                            Date = DateTime.Parse("2019-11-08")
                        },
                        new PlannedExpense()
                        {
                            Amount = 500,
                            Kind = ExpenseKind.Food
                        },
                        new PlannedExpense()
                        {
                            Amount = 500,
                            Kind = ExpenseKind.Fuel
                        },
                    };
                    await _budgetService.CreateBudgetAsync(budget);
                    return RedirectToAction("Index", "Budget");
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(budget);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _budgetService.GetBudgetByIdAsync(id));
        }
    }
}
