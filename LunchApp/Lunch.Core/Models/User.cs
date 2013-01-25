using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("Users")]
    public class User : Template.User
    {
        #region DatabaseFields

        [Key]
        public override int Id { get; set; }
        public override string FullName { get; set; }
        public override string Email { get; set; }
        public override bool SendMail1 { get; set; }
        public override bool SendMail2 { get; set; }
        public override bool SendMail3 { get; set; }
        public override bool SendMail4 { get; set; }
        public override Guid? GUID { get; set; }
        #endregion

        #region RelatedTables
        public List<Models.Restaurant> Restaurant { get; set; }
        #endregion

        #region AdditionalFields

        [Editable(false)]
        public string FullNameWithEmail
        {
            get { return FullName + "(" + Email + ")"; }
        }

        #endregion

    }
}