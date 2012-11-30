using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunch.Core.Models
{
    public class RestaurantType
    {
        public virtual int ID { get; protected set; }
        public virtual string TypeName { get; set; }
    }
}
