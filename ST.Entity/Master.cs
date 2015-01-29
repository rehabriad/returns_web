using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST.Entity
{
    public class Master
    {
        [Key]
        public string rin { get; set; }
        public int officeid { get; set; }

        [StringLength (512)]
        public string regname { get; set; }

        [StringLength(512)]
        public string tradename { get; set; }
        public DateTime regdate { get; set; }

        public regstatuscode regstatuscode { get; set; }

        public fillingcode fillingcode { get; set; }



    }
}