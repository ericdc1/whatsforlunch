using FluentNHibernate.Mapping;
using Lunch.Core.Models;

namespace Lunch.Data.Mappings
{
    public class JobMap : ClassMap<Job>
    {
        public JobMap()
        {
            Table("Jobs");

            Id(x => x.JobID);

            Map(x => x.MethodName).Length(100).Not.Nullable();
            Map(x => x.ParametersJson).Length(1000).Nullable();
            Map(x => x.RunDate).Not.Nullable();
            Map(x => x.CreatedDate).Not.Nullable();
            Map(x => x.HasRun).Not.Nullable();


            HasMany(x => x.JobLogs)
                .Access.Property()
                .AsBag()
                .Cascade.None();
        }
    }
}
