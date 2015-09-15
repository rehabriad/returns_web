using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ST.Entity
{
    //public enum taxrate { محلية = 14, صادرات }
    public enum taxrate { خدمات=2, سلع=1,سلع_رأس_مالية=3 }

    public class Retpurch
    {

        public Retpurch()
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
        [Display(Name = "المدخلات")]
        public taxrate taxrate { get; set; }
        [Display(Name = "محلي")]
        public decimal localval { get; set; }
        [Display(Name = "مستورد")]
        public decimal expval { get; set; }
        [Display(Name = "إجمالي القيمة")]
        public decimal purchval { get; set; }
        [Display(Name = "ضريبة المدخلات")]
        public decimal purchtax { get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }

    }
}