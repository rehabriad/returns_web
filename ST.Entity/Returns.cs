using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ST.Entity
{
    public class Returns
    {
        public Returns()
        {
            retsale = new List<Retsale>();
            retpurch = new List<Retpurch>();
            retcapital=new List<RetCapital>();
            retreceit=new List<RetReceit>();
            retlocalpurch=new List<RetLocalPurch>();
            retexpurch=new List<RetExPurch>();
            moddate = DateTime.Now;
            transdate = DateTime.Now;
            taxyrmo = DateTime.Now.AddMonths(-1);
            docLocNumber = "";
            returncode = returnCode.مبيعات;
            officeid = 83;
            saleltc = 0;
            nettaxpy = 0;
            purctdt2 = 0;
            status = ReturnStatus.Pending;
        }

        [Key()]
        public Guid Id { get; set; }

        [Required()]
        public int officeid { get; set; }

        [StringLength(9)]
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        [Display(Name = "rinDisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
        public string rin { get; set; }

        [ForeignKey("rin")]
        Master master { get; set; }

        [Required()]

        [Display(Name = "returncodeDisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
        public returnCode returncode { get; set; }

        [Display(Name = "taxyrmoDisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public DateTime taxyrmo { get; set; }

        [Display(Name = "transdateDisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public DateTime transdate { get; set; }

        [Display(Name = "saleltcDisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public decimal saleltc { get; set; }

        [Display(Name = "purctdt2DisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public decimal purctdt2 { get; set; }

        [Display(Name = "nettaxpyDisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public decimal nettaxpy { get; set; }

        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public int targetoffid { get; set; }

        [StringLength(20)]
        [Display(Name = "doclocnumDisplayName", ResourceType = typeof(ST.Resource.ReturnsResource))]
       //[Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public string docLocNumber { get; set; }

        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public DateTime moddate { get; set; }

        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(ST.Resource.ReturnsResource))]
        public ReturnStatus status { get; set; }

        public virtual ICollection<Retsale> retsale { get; set; }
        public virtual ICollection<Retpurch> retpurch { get; set; }
        public virtual ICollection<RetCapital> retcapital { get; set; }
        public virtual ICollection<RetReceit> retreceit { get; set; }
        public virtual ICollection<RetLocalPurch> retlocalpurch { get; set; }
        public virtual ICollection<RetExPurch> retexpurch { get; set; }
    }
}