using System;
using System.Collections.Generic;
using System.Text;

namespace LockCommons.Models.Mq
{
    public class LockActivityCheck
    {
        public LockActivityCheck()
        {

        }
        public string LockDeviceId { get; set; }
        public DateTime EventDate { get; set; }
    }
}
