using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Models
{
    public class PlannedRevenue
    {
        [Key]
        public virtual int Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public virtual decimal Amount { get; set; }
        [Required]
        public virtual RevenueKind Kind { get; set; }
        public virtual DateTime? Date { get; set; }
    }
}
