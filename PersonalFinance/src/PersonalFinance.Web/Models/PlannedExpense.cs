using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Models
{
    public class PlannedExpense
    {
        [Key]
        public virtual int Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public virtual decimal Amount { get; set; }
        [Required]
        public virtual ExpenseKind Kind { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string Code { get; set; }
    }
}
