using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    public class User : Database.User
    {


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