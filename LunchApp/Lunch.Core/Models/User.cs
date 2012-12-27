using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lunch.Core.Models
{
    [Table("Users")]
    public class User
    {
        #region DatabaseFields

        [Key]
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Email { get; set; }
        public virtual bool SendMail1 { get; set; }
        public virtual bool SendMail2 { get; set; }
        public virtual bool SendMail3 { get; set; }
        public virtual bool SendMail4 { get; set; }
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