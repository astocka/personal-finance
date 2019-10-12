using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalFinance.Web.Models;

namespace PersonalFinance.Web.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int expenseId);
        Task<bool> CreateExpenseAsync(Expense expense);
        Task<bool> ImportExpensesAsync();
        Task<bool> UpdateExpenseAsync(Expense expenseToUpdate);
        Task<bool> DeleteExpenseAsync(int? expenseId);
    }
}
