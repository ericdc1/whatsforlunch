﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("User")]
    public class User
    {
        #region DatabaseFields

        [Key]
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual bool SendMorningEmailFlg { get; set; }
        public virtual bool SendVotingIsOverEmailFlg { get; set; }
        public virtual bool SendWhereWeAreGoingEmailFlg { get; set; }
        public virtual bool IsAdministrator { get; set; }
        public virtual string GUID { get; set; }
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