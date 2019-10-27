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

        [HttpGet]
        [Route("Budget/{budgetId}/CreatePlannedExpense")]
        public IActionResult CreatePlannedExpense(int budgetId)
        {
            ViewData["BudgetId"] = budgetId;
            return View();
        }

        [HttpPost]
        [Route("Budget/{budgetId}/CreatePlannedExpense")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlannedExpense(int budgetId, [Bind("Id,Amount,Kind,Date")]PlannedExpense plannedExpense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    plannedExpense.Amount = (decimal)plannedExpense.Amount;
                    await _budgetService.CreateBudgetExpenseAsync(budgetId, plannedExpense);
                    return RedirectToAction("Details", "Budget", new { id = budgetId });
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(plannedExpense);
        }

        [HttpGet]
        [Route("Budget/{budgetId}/CreatePlannedRevenue")]
        public IActionResult CreatePlannedRevenue(int budgetId)
        {
            ViewData["BudgetIdR"] = budgetId;
            return View();
        }

        [HttpPost]
        [Route("Budget/{budgetId}/CreatePlannedRevenue")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlannedRevenue(int budgetId, [Bind("Id,Amount,Kind,Date")]PlannedRevenue plannedRevenue)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    plannedRevenue.Amount = (decimal)plannedRevenue.Amount;
                    await _budgetService.CreateBudgetRevenueAsync(budgetId, plannedRevenue);
                    return RedirectToAction("Details", "Budget", new { id = budgetId });
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(plannedRevenue);
        }
    }
}
