using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace returns_web.Models
{
    public enum ReturnStatus { Pending = 1, Colsed=2,Sent=3 }
    public class Returns
    {
        public Returns()
        {
            retsale = new List<Retsale>();
            retpurch = new List<Retpurch>();
            moddate = DateTime.Now;
            transdate=DateTime.Now;
            taxyrmo = DateTime.Now;
            
            returncode = 1;
            officeid = 83;
            status=ReturnStatus.Pending;
        }
        public Guid Id { get; set; }
        public int officeid { get; set; }
        
        [Display(Name = "رقم التسجيل")]
        public string rin  { get; set; }
        [ForeignKey("rin")]
        Master master { get; set; }
        public int returncode  { get; set; }
         
        [Display(Name = "الفترة الضريبية")]
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}", ApplyFormatInEditMode = true)] 
        public DateTime taxyrmo { get; set; }
       
        [Display(Name = "تاريخ المعاملة")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)] 
        public DateTime transdate { get; set; }
         [Display(Name = "المبيعات")]
        public decimal saleltc { get; set; }
         [Display(Name = "المشتريات")]
        public decimal purctdt2 { get; set; }
         [Display(Name = "صافى الضريبة")]
        public decimal nettaxpy { get; set; }
        public int targetoffid { get; set; }

        public enum taxratesflg { one_cat=0,group_cat}
         [Display(Name = "الكود")]
        public string doclocnum { get; set; }

        public DateTime moddate { get; set; }

        public ReturnStatus status { get; set; }

        public string docLocNumber { get; set; }
        public virtual ICollection<Retsale> retsale { get; set; }
        public virtual ICollection<Retpurch> retpurch { get; set; }
    }
}