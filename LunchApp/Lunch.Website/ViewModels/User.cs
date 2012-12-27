using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lunch.Website.ViewModels
{
    public class User
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }
        [DisplayName("Name")]
        [Required]
        public virtual string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }
        [DisplayName("Morning Email")]
        public virtual bool SendMail1 { get; set; }
        [DisplayName("Voting Over Email")]
        public virtual bool SendMail2 { get; set; }
        [DisplayName("Where To Email")]
        public virtual bool SendMail3 { get; set; }
        [DisplayName("After Lunch To Email")]
        public virtual bool SendMail4 { get; set; }
        
        [DisplayName("Key")]
        public virtual string GUID { get; set; }

    }
}