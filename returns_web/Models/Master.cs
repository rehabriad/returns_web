using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace returns_web.Models
{
    public class Master
    {
        [Key]
        public string rin { get; set; }
        public int officeid { get; set; }

        public string regname { get; set; }
        public string tradename { get; set; }
        public DateTime regdate { get; set; }
        public enum regstatuscode {new_reg=1,re_reg,re_enter,trans,expired,exp_npret,exp_credit}
        public enum fillingcode {sales=0,table,sale_table }



    }
}