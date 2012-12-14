using FluentNHibernate.Mapping;
using Lunch.Core.Models;

namespace Lunch.Data.Mappings
{
    public class RestaurantTypeMap : ClassMap<RestaurantType>
    {
         public RestaurantTypeMap()
         {
             Table("RestaurantTypes");

             Id(x => x.RestaurantTypeID);

             Map(x => x.TypeName).Length(50).Not.Nullable();

             HasMany<Restaurant>(x => x.Restaurants)
                 .KeyColumn("RestaurantTypeID")
                 .Access.Property()
                 .AsBag();
         }
    }
}