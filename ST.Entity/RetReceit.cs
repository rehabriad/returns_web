using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Entity
{
    public class RetReceit
    {
        public enum Receiptcodes { فواتير_ضريبية=1,إشعارات_إضافية=2,إشعارات_خصم=3,اخرى_تذكرة=4  }

        public RetReceit()
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
        [Display(Name = "نوع")]
        public Receiptcodes recietcode { get; set; }
        [Display(Name = "عدد")]
        public int recietno{ get; set; }
        [Display(Name = "رقم المسلسل من")]
        public decimal recietserialfrom { get; set; }
        [Display(Name = "رقم المسلسل الى")]
        public decimal recietserialto{ get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }

    }
    
}
