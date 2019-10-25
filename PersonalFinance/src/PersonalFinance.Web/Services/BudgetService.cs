using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        public async Task<List<Budget>> GetBudgetAsync()
        {
            return await _dataContext.Budgets.OrderByDescending(o => o.Year).OrderByDescending(m => m.Month).ToListAsync();
        }

        public async Task<Budget> GetBudgetByIdAsync(int budgetId)
        {
            return await _dataContext.Budgets.Include(r => r.Revenues).Include(e => e.Expenses).FirstOrDefaultAsync(x => x.Id == budgetId); ;
        }

        public async Task<bool> CreateBudgetAsync(Budget budget)
        {
            if (budget == null)
            {
                return false;
            }
            _dataContext.Budgets.Add(budget);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public Task<bool> DeleteBudgetAsync(int? budgetId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBudgetAsync(Budget budgetToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateBudgetExpenseAsync(PlannedExpense plannedExpense)
        {
            if (plannedExpense == null)
            {
                return false;
            }
            var created = 0;
            using (var trans = await _dataContext.Database.BeginTransactionAsync())
            {
                _dataContext.PlannedExpenses.Add(plannedExpense);
                _dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT BudgetDb.dbo.PlannedExpenses ON;");

                created = await _dataContext.SaveChangesAsync();
                _dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT BudgetDb.dbo.PlannedExpenses OFF;");

                trans.Commit();
            }
            return created > 0;
        }
    }
}
