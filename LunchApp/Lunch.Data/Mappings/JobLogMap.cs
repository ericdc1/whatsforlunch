using FluentNHibernate.Mapping;
using Lunch.Core.Models;

namespace Lunch.Data.Mappings
{
    public class JobLogMap : ClassMap<JobLog>
    {
        public JobLogMap()
        {
            Table("JobLogs");

            Id(x => x.JobLogID);

            Map(x => x.Category).Length(100).Not.Nullable();
            Map(x => x.Message).Length(100).Nullable();
            Map(x => x.LogDTM).Not.Nullable();


            References(x => x.Job)
                .Column("JobID")
                .Access.Property();
            Map(x => x.JobID).Formula("JobID")
                .Not.Nullable();
        } 
    }
}