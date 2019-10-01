using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Web.Data;
using PersonalFinance.Web.Helpers;
using PersonalFinance.Web.Models;
using PersonalFinance.Web.Services.Interfaces;

namespace PersonalFinance.Web.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly DataContext _dataContext;

        public ExpenseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await _dataContext.Expenses.OrderByDescending(o => o.Date).ToListAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(Guid expenseId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ImportExpensesAsync()
        {
            var fileHelper = new FileHelper();
            var transactions = fileHelper.GetTransactions();
            var expenses = fileHelper.GetExpenses(transactions);

            var mapExpenses = new List<Expense>();

            foreach (var expense in expenses)
            {
                mapExpenses.Add(new Expense
                {
                    Id = expense.Id,
                    Date = expense.Date,
                    Amount = expense.Amount,
                    Description = expense.Description,
                    Notes = expense.Notes,
                    Payee = expense.Payee,
                    Type = ExpenseType.Card
                });
            }
            foreach (var mapExpense in mapExpenses)
            {
                if (!await _dataContext.Expenses.AnyAsync(x => x.Amount == mapExpense.Amount && x.Date == mapExpense.Date))
                {
                    await _dataContext.Expenses.AddAsync(mapExpense);
                }
            }
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public Task<bool> UpdateExpenseAsync(Expense expenseToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteExpenseAsync(Guid expenseId)
        {
            throw new NotImplementedException();
        }
    }
}
