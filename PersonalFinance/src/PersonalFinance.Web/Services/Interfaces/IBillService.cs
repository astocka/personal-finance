using PersonalFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Services.Interfaces
{
    public interface IBillService
    {
        Task<List<Bill>> GetBillsAsync();
        Task<Bill> GetBillByIdAsync(int? billId);
        Task<bool> CreateBillAsync(Bill bill);
        Task<bool> UpdateBillAsync(Bill billToUpdate);
        Task<bool> DeleteBillAsync(int? billId);
    }
}
