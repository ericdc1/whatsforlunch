using FluentNHibernate.Mapping;
using Lunch.Core.Models;

namespace Lunch.Data.Mappings
{
    public class RestaurantMap : ClassMap<Restaurant>
    {
        public RestaurantMap()
        {
            Table("Restaurants");

            Id(x => x.RestaurantID);

            Map(x => x.RestaurantName).Length(50).Not.Nullable();
            Map(x => x.PreferredDayOfWeek).Nullable();

            References(x => x.RestaurantType)
                .Column("RestaurantTypeID")
                .Access.Property();
            Map(x => x.RestaurantTypeID).Formula("RestaurantTypeID")
                .Not.Nullable();


            //HasMany(x => x.RestaurantHistories)
            //    .Access.Property()
            //    .AsList()
            //    .Cascade.None();
        }
    }
}