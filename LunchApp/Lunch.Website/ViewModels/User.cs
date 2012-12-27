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
        public virtual string Password { get; set; }
        [DisplayName("Morning Mail")]
        public virtual bool SendMorningEmailFlg { get; set; }
        [DisplayName("Voting Over Mail")]
        public virtual bool SendVotingIsOverEmailFlg { get; set; }
        [DisplayName("Where To Email")]
        public virtual bool SendWhereWeAreGoingEmailFlg { get; set; }
        [DisplayName("Admin")]
        public virtual bool IsAdministrator { get; set; }
        [DisplayName("Key")]
        public virtual string GUID { get; set; }

    }
}