﻿using System;
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

        public async Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            return await _dataContext.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
        }

        public async Task<bool> CreateExpenseAsync(Expense expense)
        {
            if (expense == null)
            {
                return false;
            }
            _dataContext.Expenses.Add(expense);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
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
        public async Task<bool> DeleteExpenseAsync(int? expenseId)
        {
            if (expenseId == null)
            {
                return false;
            }

            Expense expenseToDelete = await GetExpenseByIdAsync((int)expenseId);

            if (expenseToDelete == null)
            {
                return false;
            }
            _dataContext.Expenses.Remove(expenseToDelete);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;

        }

        public async Task<bool> UpdateExpenseAsync(Expense expenseToUpdate)
        {
            if (expenseToUpdate == null)
            {
                return false;
            }
            _dataContext.Expenses.Update(expenseToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<List<Expense>> SearchExpensesAsync(string search)
        {
            var searchExpenses = await _dataContext.Expenses.Where(x => x.Description.Contains(search) || x.Notes.Contains(search) || x.Payee.Contains(search)).ToListAsync();
            return searchExpenses;
        }

    }
}
