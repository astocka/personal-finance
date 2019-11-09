using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Web.Data;
using PersonalFinance.Web.Models;

namespace PersonalFinance.Web.Services.Interfaces
{
    public class BillService : IBillService
    {

        private readonly DataContext _dataService;

        public BillService(DataContext dataService)
        {
            _dataService = dataService;
        }

        public async Task<bool> CreateBillAsync(Bill bill)
        {
            if (bill == null)
            {
                return false;
            }
            _dataService.Bills.Add(bill);
            var created = await _dataService.SaveChangesAsync();
            return created > 0;
        }

        public Task<bool> DeleteBillAsync(int? billId)
        {
            throw new NotImplementedException();
        }

        public Task<Bill> GetBillByIdAsync(int? billId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Bill>> GetBillsAsync()
        {
            return await _dataService.Bills.ToListAsync();
        }

        public Task<bool> UpdateBillAsync(Bill billToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
