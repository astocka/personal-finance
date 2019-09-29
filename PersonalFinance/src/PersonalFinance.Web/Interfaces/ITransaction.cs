using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Interfaces
{
   public interface ITransaction
    {
          Guid Id { get; set; }
          DateTime Date { get; set; }
          decimal Amount { get; set; }
          string Description { get; set; }
          string Notes { get; set; }
    }
}
