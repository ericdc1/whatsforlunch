using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    /// <summary>
    /// Defines business model entites which are connected/dependent on User model.
    /// </summary>
    [Flags]
    public enum UserDependencies
    {
        Vetoes = 1
    }

    public class User : Database.User
    {
        #region RelatedTables

        public List<Models.Restaurant> Restaurant { get; set; }
        public List<Models.Veto> Vetoes { get; set; }

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