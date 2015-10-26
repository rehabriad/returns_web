using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Entity
{
    public enum localtaxrate { مستوردة = 2, محلية = 1 }
    public class RetLocalPurch
    {
        public RetLocalPurch()
        {
            moddate = DateTime.Now;
            recietdate = DateTime.Now;
            officeid = 83;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int officeid { get; set; }


        public Guid returnsid { get; set; }
        [ForeignKey("returnsid")]
        Returns returns { get; set; }
        [Display(Name = "اسم المورد")]
        public string supplyname{ get; set; }
        [Display(Name = "رقم التسجيل")]
        public string rin { get; set; }
        [Display(Name = "سلع")]
        public decimal deppurch { get; set; }
        [Display(Name = "خدمة")]
        public decimal nondispurch { get; set; }
        [Display(Name = "سلع رأس مالية")]
        public decimal indeppurch{ get; set; }
        [Display(Name = "فئة الضريبة")]
        public localtaxrate taxrateid { get; set; }
        [Display(Name = "ضريبة القيمة المضافة")]
        public decimal vattax { get; set; }
        [Display(Name = "رقم الفاتورة")]
        public int recietserial { get; set; }
        [Display(Name = "تاريخ الفاتورة")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime recietdate  { get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }

    }
    
}
