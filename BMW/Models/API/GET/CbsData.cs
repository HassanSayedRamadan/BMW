using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMW.Models.API.GET
{
    public class CbsData
    {
        public string cbsType { get; set; }
        public string cbsState { get; set; }
        public int? cbsRemainingMileage { get; set; }
        public string cbsDueDate { get; set; }
        public string cbsDescription { get; set; }
    }
}