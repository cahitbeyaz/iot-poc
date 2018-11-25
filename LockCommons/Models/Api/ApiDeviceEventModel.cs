using System;
using System.Collections.Generic;
using System.Text;
using static LockCommons.Models.Proto.LockEvent.Types;

namespace LockCommons.Models.Api
{
    public class ApiDeviceEventModel
    {
        public DateTime EventTime { get; set; }
        public string RequestReferenceNumber { get; set; }
        public string DeviceEvent { get; set; }
    }
}
