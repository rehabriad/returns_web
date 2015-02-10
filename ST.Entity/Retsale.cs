using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ST.Entity
{
    public enum taxrateid { صفرى_صادرات = 9, إعفاء, محلية = 14 }
    public class Retsale
    {
        public Retsale()
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
         [Display(Name = "النوع")]
        public taxrateid taxrateid { get; set; }
         [Display(Name = "قيمة المبيعات")]
        public decimal saleval { get; set; }
         [Display(Name = "الضريبة")]
        public decimal saletax { get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }


        
    }
}