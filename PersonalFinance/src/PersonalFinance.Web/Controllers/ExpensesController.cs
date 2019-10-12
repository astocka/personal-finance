using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Web.Models;
using PersonalFinance.Web.Services.Interfaces;

namespace PersonalFinance.Web.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _expenseService.GetExpensesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ImportExpenses()
        {
            var result = await _expenseService.ImportExpensesAsync();
            if (!result)
            {
                TempData["ExpensesExist"] = "Expenses already exist in database!";
            }
            return RedirectToAction("Index", "Expenses");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _expenseService.DeleteExpenseAsync(id);
            return RedirectToAction("Index", "Expenses");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Expense expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Date,Amount,Description,Payee,Notes,Type")]Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _expenseService.UpdateExpenseAsync(expense);
                return RedirectToAction("Index", "Expenses");
            }
            return View(expense);
        }
    }
}
