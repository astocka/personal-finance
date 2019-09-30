using PersonalFinance.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Web.Models
{
    public class Income : ITransaction
    {
        [Key]
        public virtual Guid Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public virtual DateTime Date { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required]
        public virtual decimal Amount { get; set; }
        public virtual string Description { get; set; }
        public virtual string Payor { get; set; }
        public virtual string Notes { get; set; }
    }
}
