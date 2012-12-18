using Lunch.Core.Models;

namespace Lunch.Data
{
    public class LunchDatabase : Dapper.Database<LunchDatabase>
    {
        public Table<Restaurant> Restaurants { get; private set; }
        public Table<RestaurantType> RestaturantTypes{ get; private set; }
        public Table<JobLog> JobLogs { get; private set; }
        public Table<Job> Jobs { get; private set; }

    }
}