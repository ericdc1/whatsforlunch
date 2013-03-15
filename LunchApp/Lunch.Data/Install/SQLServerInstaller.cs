using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunch.Data.Install
{
    class SqlServerInstaller
    {
        /// <summary>
        /// Returns the sql to do a full install
        /// </summary>
        protected string FullInstallSql
        {
            get { return SQLresources._0001_InitialLoad; }
        }

        //we need some sort of upgrade method

    }
}
