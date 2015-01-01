using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace returns_web.Models
{
    public enum taxrate { محلية = 14, صادرات }

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
        [Display(Name = "النوع")]
        public taxrate taxrate { get; set; }
         [Display(Name = "قيمة المشتربات")]
        public decimal purchval { get; set; }
          [Display(Name = "الضريبة")]
        public decimal purchtax { get; set; }

        public int targetoffid { get; set; }

        public DateTime moddate { get; set; }

    }
}