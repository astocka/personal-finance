using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinance.Web.Models;

namespace PersonalFinance.Web.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(Guid expenseId);
        Task<bool> ImportExpensesAsync();
        Task<bool> UpdateExpenseAsync(Expense expenseToUpdate);
        Task<bool> DeleteExpenseAsync(Guid expenseId);
    }
}
