using System;
using System.Collections.Generic;
using System.Text;
using static LockCommons.Models.Proto.LockEvent.Types;

namespace LockCommons.Models
{
    public static class ModelUtils
    {
        public static DeviceEventEnum StringEvent2Enum(string cmd)
        {
            switch (cmd)
            {
                case "open":
                    return DeviceEventEnum.Open;
                case "close":
                    return DeviceEventEnum.Close;
                case "tamper":
                    return DeviceEventEnum.Tamper;

                default:
                    throw new Exception("event is not supported");
            }
        }
    }
}
