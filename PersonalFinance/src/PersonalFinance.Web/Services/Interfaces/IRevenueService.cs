using PersonalFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Services.Interfaces
{
    public interface IRevenueService
    {
        Task<List<Revenue>> GetRevenuesAsync();
        Task<Revenue> GetRevenueByIdAsync(int revenueId);
        Task<bool> CreateRevenueAsync(Revenue revenue);
        Task<bool> ImportRevenueAsync();
        Task<bool> UpdateRevenueAsync(Revenue revenueToUpdate);
        Task<bool> DeleteRevenueAsync(int? revenueId);
        Task<List<Revenue>> SearchRevenuesAsync(string search);
    }
}
