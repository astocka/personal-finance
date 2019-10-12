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
    public class RevenueService : IRevenueService
    {
        private readonly DataContext _dataContext;

        public RevenueService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Revenue>> GetRevenuesAsync()
        {
            return await _dataContext.Revenues.OrderByDescending(o => o.Date).ToListAsync();
        }

        public Task<bool> CreateRevenueAsync(Revenue revenue)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRevenueAsync(int? revenueId)
        {
            throw new NotImplementedException();
        }

        public Task<Revenue> GetRevenueByIdAsync(int revenueId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ImportRevenuesync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRevenueAsync(Revenue revenueToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
