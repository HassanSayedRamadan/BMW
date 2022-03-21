using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMW.Models.API.GET
{
    public class Position
    {
        public double? lat { get; set; }
        public double? lon { get; set; }
        public int? heading { get; set; }
        public string status { get; set; }
    }
}