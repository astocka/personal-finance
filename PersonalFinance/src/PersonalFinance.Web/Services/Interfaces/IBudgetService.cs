using PersonalFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<List<Budget>> GetBudgetAsync();
        Task<Budget> GetBudgetByIdAsync(int budgetId);
        Task<bool> CreateBudgetAsync(Budget budget);
        Task<bool> UpdateBudgetAsync(Budget budgetToUpdate);
        Task<bool> DeleteBudgetAsync(int? budgetId);
        Task<bool> CreateBudgetExpenseAsync(int budgetId, PlannedExpense plannedExpense);
        Task<bool> CreateBudgetRevenueAsync(int budgetId, PlannedRevenue plannedRevenue);
    }
}
