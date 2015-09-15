using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ST.Entity
{
    public enum taxrateid { صفرى_صادرات = 9, إعفاء=10, محلية = 14 }
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
        [Display(Name = "فئة الضريبة")]
        public taxrateid taxrateid { get; set; }
        [Display(Name = "السلعة")]
        public decimal goodval { get; set; }
        [Display(Name = "الخدمات")]
        public decimal serviceval { get; set; }

        [Display(Name = "اجمالي القيمة")]
        public decimal saleval { get; set; }
        [Display(Name = "ضريبة القيمة المضافة")]
        public decimal saletax { get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }



    }
}