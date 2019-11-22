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

        public async Task<Budget> GetBudgetByIdAsync(int? budgetId)
        {
            if (budgetId == null)
            {
                return new Budget();
            }
            return await _dataContext.Budgets.Include(r => r.Revenues).Include(e => e.Expenses).FirstOrDefaultAsync(x => x.Id == budgetId);
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

        public async Task<bool> DeleteBudgetAsync(int? budgetId)
        {
            if (budgetId == null)
            {
                return false;
            }
            var budget = await GetBudgetByIdAsync(budgetId);
            _dataContext.Remove(budget);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }


        public async Task<bool> UpdateBudgetAsync(Budget budgetToUpdate)
        {
            if (budgetToUpdate == null)
            {
                return false;
            }
            _dataContext.Update(budgetToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> CreateBudgetExpenseAsync(int budgetId, PlannedExpense plannedExpense)
        {
            if (plannedExpense == null)
            {
                return false;
            }
            var created = 0;
            using (var trans = await _dataContext.Database.BeginTransactionAsync())
            {
                var budget = _dataContext.Budgets.FirstOrDefault(x => x.Id == budgetId);
                plannedExpense.Budget = budget;


                _dataContext.PlannedExpenses.Add(plannedExpense);
                budget.Expenses.Add(plannedExpense);

                created = await _dataContext.SaveChangesAsync();

                trans.Commit();
            }
            return created > 0;
        }

        public async Task<bool> CreateBudgetRevenueAsync(int budgetId, PlannedRevenue plannedRevenue)
        {
            if (plannedRevenue == null)
            {
                return false;
            }
            var createdRevenue = 0;
            using (var trans = await _dataContext.Database.BeginTransactionAsync())
            {
                var budget = _dataContext.Budgets.FirstOrDefault(x => x.Id == budgetId);
                plannedRevenue.Budget = budget;

                _dataContext.PlannedRevenues.Add(plannedRevenue);
                budget.Revenues.Add(plannedRevenue);

                createdRevenue = await _dataContext.SaveChangesAsync();

                trans.Commit();
            }
            return createdRevenue > 0;
        }

        public async Task<PlannedExpense> GetPlannedExpenseByIdAsync(int? plannedExpenseId)
        {
            if (plannedExpenseId == null)
            {
                return new PlannedExpense();
            }
            return await _dataContext.PlannedExpenses.Include(x => x.Budget).FirstOrDefaultAsync(x => x.Id == plannedExpenseId);
        }

        public async Task<bool> UpdatePlannedExpenseAsync(PlannedExpense plannedExpense)
        {
            if (plannedExpense == null)
            {
                return false;
            }
            _dataContext.PlannedExpenses.Update(plannedExpense);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<PlannedRevenue> GetPlannedRevenueByIdAsync(int? plannedRevenueId)
        {
            if (plannedRevenueId == null)
            {
                return new PlannedRevenue();
            }
            return await _dataContext.PlannedRevenues.Include(x => x.Budget).FirstOrDefaultAsync(x => x.Id == plannedRevenueId);
        }

        public async Task<bool> UpdatePlannedRevenueAsync(PlannedRevenue plannedRevenue)
        {
            if (plannedRevenue == null)
            {
                return false;
            }
            _dataContext.PlannedRevenues.Update(plannedRevenue);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeletePlannedRevenueAsync(int? plannedRevenueId)
        {
            if (plannedRevenueId == null)
            {
                return false;
            }
            var plannedRevenue = await GetPlannedRevenueByIdAsync(plannedRevenueId);
            _dataContext.Remove(plannedRevenue);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

    }
}
