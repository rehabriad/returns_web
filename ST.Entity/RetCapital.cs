using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Entity
{
    public class RetCapital
    {
        public RetCapital()
        {
            moddate = DateTime.Now;
            officeid = 83;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int officeid { get; set; }


        public Guid returnsid { get; set; }
        [ForeignKey("returnsid")]
        Returns returns { get; set; }
        [Display(Name = "رصيد سابق")]
        public decimal prevbalance{ get; set; }
        [Display(Name = "ضريبة مسددة خلال شهر")]
        public decimal paidtax{ get; set; }
        [Display(Name = "الإجمالي")]
        public decimal totalval { get; set; }
        [Display(Name = "ضريبة محصومة في حدود الضريبة المستحقة")]
        public decimal discounttax { get; set; }
        [Display(Name = "ضريبة غير قابلة للخصم")]
        public decimal nondiscounttax { get; set; }
        [Display(Name = "رصيد مرحل")]
        public decimal shiftbalance { get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }

    }
    
}
