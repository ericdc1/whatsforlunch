using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace Lunch.Website.Models
{
    public class JobLogRepository
    {
        public IEnumerable<JobLog> GetList()
        {
            IEnumerable<JobLog> result;
            using (var conn = Helpers.Db.GetOpenConnection())
            {
                result = conn.Query<JobLog>("Select * from JobLog");
            }
            return result;
        }

        public bool Insert(JobLog model)
        {
            using (var conn = Helpers.Db.GetOpenConnection())
            {
                conn.Execute("Insert into JobLog (LogDTM, Category, Message) Values (@LogDTM, @Category, @Message)", model);
            }
            return true;
        }



    }
}