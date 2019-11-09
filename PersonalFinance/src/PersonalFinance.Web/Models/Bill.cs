using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Models
{
    public class Bill
    {
        [Key]
        public virtual int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public virtual decimal Amount { get; set; }
        [DataType(DataType.Date)]
        public virtual DateTime? Date { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public virtual DateTime DueDate { get; set; }
        [DataType(DataType.Date)]
        public virtual DateTime? PaidDate { get; set; }
        public virtual bool IsPaid { get; set; }
        public virtual string Notes { get; set; }
    }
}
