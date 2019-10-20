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
            List<ExpenseKind> expenseKinds = Enum.GetValues(typeof(ExpenseKind)).Cast<ExpenseKind>().ToList();
            ViewData["ExpenseKinds"] = expenseKinds;
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
                    await _budgetService.CreateBudgetAsync(budget);
                    return NoContent();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExpense([Bind("Id,Amount,Kind,Date,Code")]PlannedExpense plannedExpense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    plannedExpense.Amount = (decimal)plannedExpense.Amount;
                    await _budgetService.CreateBudgetExpenseAsync(plannedExpense);
                    return NoContent();
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(plannedExpense);
        }
    }
}
