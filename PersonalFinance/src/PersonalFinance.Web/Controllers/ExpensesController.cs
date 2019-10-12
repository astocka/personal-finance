using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
