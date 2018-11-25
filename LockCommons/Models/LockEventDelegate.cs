using LockCommons.Models.Proto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LockCommons.Models
{
    public delegate Task<bool> ProcessLockEvent(LockEvent lockEvent);

    public class DeliveryEventHandler
    {
        public event ProcessLockEvent LockEventProcessHandler;
        public DeliveryEventHandler()
        {
        }

        public async Task<bool> ProcessEvent(byte[] data)
        {
            LockEvent lockEvent= LockEvent.Parser.ParseFrom(data);
            return await LockEventProcessHandler.Invoke(lockEvent);
        }
    }
}
