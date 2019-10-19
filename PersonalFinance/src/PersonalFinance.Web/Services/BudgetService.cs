using Microsoft.EntityFrameworkCore;
using PersonalFinance.Web.Data;
using PersonalFinance.Web.Models;
using PersonalFinance.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly DataContext _dataContext;

        public BudgetService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<bool> CreateBudgetAsync(Budget budget)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBudgetAsync(int? budgetId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Budget>> GetBudgetAsync()
        {
            return await _dataContext.Budgets.OrderByDescending(o => o.Year).OrderByDescending(m => m.Month).ToListAsync();
        }

        public Task<Budget> GetBudgetByIdAsync(int budgetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBudgetAsync(Budget budgetToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
