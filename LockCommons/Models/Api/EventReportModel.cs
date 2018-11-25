using System;
using System.Collections.Generic;
using System.Text;

namespace LockCommons.Models.Api
{
    public class LockDeviceEventReportModel
    {
        public ApiLockDevice LockDevice { get; set; }
        public IEnumerable<ApiDeviceEventModel> LockDeviceEvents { get; set; }
    }
}
