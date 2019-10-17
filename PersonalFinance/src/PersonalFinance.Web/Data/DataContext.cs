using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<PlannedExpense> PlannedExpenses { get; set; }
        public DbSet<PlannedRevenue> PlannedRevenues { get; set; }
        public DbSet<Budget> Budgets { get; set; }
    }
}
