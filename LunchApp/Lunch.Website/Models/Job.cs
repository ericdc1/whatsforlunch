﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch.Website.Models
{
    public class Job
    {
        public int ID { get; set; }
        public string MethodName { get; set; }
        public string ParametersJson { get; set; }
        public DateTime RunDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}

//ToDo: Create Add/Edit/Delete jobs