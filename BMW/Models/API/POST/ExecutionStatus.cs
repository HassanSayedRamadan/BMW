﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMW.Models.API.POST
{
    public class ExecutionStatus
    {
        public string serviceType { get; set; }
        public string status { get; set; }
        public string eventId { get; set; }
    }
}