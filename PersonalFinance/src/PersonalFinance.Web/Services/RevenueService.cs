using Microsoft.EntityFrameworkCore;
using PersonalFinance.Web.Data;
using PersonalFinance.Web.Helpers;
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

        public async Task<Revenue> GetRevenueByIdAsync(int revenueId)
        {
            return await _dataContext.Revenues.FirstOrDefaultAsync(x => x.Id == revenueId);

        }

        public Task<bool> CreateRevenueAsync(Revenue revenue)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRevenueAsync(int? revenueId)
        {
            if (revenueId == null)
            {
                return false;
            }

            Revenue revenueToDelete = await GetRevenueByIdAsync((int)revenueId);

            if (revenueToDelete == null)
            {
                return false;
            }
            _dataContext.Revenues.Remove(revenueToDelete);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }



        public async Task<bool> ImportRevenueAsync()
        {
            var fileHelper = new FileHelper();
            var transactions = fileHelper.GetTransactions();
            var revenues = fileHelper.GetRevenues(transactions);

            var mapRevenues = new List<Revenue>();

            foreach (var revenue in revenues)
            {
                mapRevenues.Add(new Revenue
                {
                    Id = revenue.Id,
                    Date = revenue.Date,
                    Amount = revenue.Amount,
                    Description = revenue.Description,
                    Notes = revenue.Notes,
                    Payer = revenue.Payer,
                });
            }
            foreach (var mapRevenue in mapRevenues)
            {
                if (!await _dataContext.Revenues.AnyAsync(x => x.Amount == mapRevenue.Amount && x.Date == mapRevenue.Date))
                {
                    {
                        await _dataContext.Revenues.AddAsync(mapRevenue);
                    }
                }
            }
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;


        }
        public Task<bool> UpdateRevenueAsync(Revenue revenueToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
