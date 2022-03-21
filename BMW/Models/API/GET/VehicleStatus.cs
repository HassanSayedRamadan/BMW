using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMW.Models.API.GET
{
    public class VehicleStatus
    {
        public string vin { get; set; }
        public int? mileage { get; set; }
        public string updateReason { get; set; }
        public DateTime? updateTime { get; set; }
        public string doorDriverFront { get; set; }
        public string doorDriverRear { get; set; }
        public string doorPassengerFront { get; set; }
        public string doorPassengerRear { get; set; }
        public string windowDriverFront { get; set; }
        public string windowDriverRear { get; set; }
        public string windowPassengerFront { get; set; }
        public string windowPassengerRear { get; set; }
        public string sunroof { get; set; }
        public string trunk { get; set; }
        public string rearWindow { get; set; }
        public string hood { get; set; }
        public string doorLockState { get; set; }
        public string parkingLight { get; set; }
        public string positionLight { get; set; }
        public int? remainingFuel { get; set; }
        public int? remainingRangeFuel { get; set; }
        public int? remainingRangeFuelMls { get; set; }
        public Position position { get; set; }
        public DateTime? internalDataTimeUTC { get; set; }
        public bool? singleImmediateCharging { get; set; }
        public string vehicleCountry { get; set; }
        public List<object> checkControlMessages { get; set; }
        public List<CbsData> cbsData { get; set; }
        public string DCS_CCH_Activation { get; set; }
        public bool? DCS_CCH_Ongoing { get; set; }
    }
}