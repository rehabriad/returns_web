using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Entity
{
    public enum recietcode { الشهادات_المستوردة=1, شهادات_البريد_السريع=2 }
    public class RetExPurch
    {
        
        public RetExPurch()
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
        [Display(Name = "نوع الشهادة")]
        public recietcode recietcode { get; set; }
        [Display(Name = "عدد الشهادات")]
        public int certno { get; set; }
        [Display(Name = "إجمالي القيمة")]
        public decimal totalvalue{ get; set; }
        [Display(Name = "فئة الضريبة")]
        public taxrate taxrateid { get; set; }
        [Display(Name = "الضريبة")]
        public decimal tax { get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }

    }
    
}
