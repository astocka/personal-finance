﻿using System;
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

        private readonly DataContext _dataContext;

        public BillService(DataContext dataService)
        {
            _dataContext = dataService;
        }

        public async Task<bool> CreateBillAsync(Bill bill)
        {
            if (bill == null)
            {
                return false;
            }
            _dataContext.Bills.Add(bill);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public Task<bool> DeleteBillAsync(int? billId)
        {
            throw new NotImplementedException();
        }

        public async Task<Bill> GetBillByIdAsync(int? billId)
        {
          if (billId == null)
            {
                return new Bill();
            }
            return await _dataContext.Bills.FirstOrDefaultAsync(x => x.Id == billId);
        }

        public async Task<List<Bill>> GetBillsAsync()
        {
            return await _dataContext.Bills.ToListAsync();
        }

        public async Task<bool> UpdateBillAsync(Bill billToUpdate)
        {
            if (billToUpdate == null)
            {
                return false;
            }
            _dataContext.Update(billToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}