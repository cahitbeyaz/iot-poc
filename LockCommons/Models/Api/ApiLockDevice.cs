using System;
using System.Collections.Generic;
using System.Text;

namespace LockCommons.Models.Api
{
    public class ApiLockDevice
    {
        public string LockDeviceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastActiveTime { get; set; }
    }
}
