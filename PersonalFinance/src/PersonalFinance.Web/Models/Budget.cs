using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Models
{
    public class Budget
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual int Month { get; set; }
        public virtual int Year { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public virtual decimal TotalRevenue { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public virtual decimal PlannedExpenses { get; set; }
        public virtual List<PlannedRevenue> Revenues { get; set; }
        public virtual List<PlannedExpense> Expenses { get; set; }
    }
}
