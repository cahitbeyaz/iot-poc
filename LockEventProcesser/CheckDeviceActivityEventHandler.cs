using LockCommons.DB;
using LockCommons.Models.Mq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using LockCommons.Models;

namespace LockEventGateway
{

    public class CheckDeviceActivityEventHandler : IEventHandler
    {
        public event ProcessEventDelegate LockEventProcessHandler;

        public CheckDeviceActivityEventHandler()
        {
            LockEventProcessHandler += CheckEvent;
        }

        private async Task<bool>  CheckEvent(object lockActivityCheckEventobj)
        {
            try
            {
                LockActivityCheck lockActivityCheckEvent = (LockActivityCheck)lockActivityCheckEventobj;
                //check if there is newer activity if no deactivate device
                var events = await MongoDriver.MongoDbRepo.GetLockEventBsonsByField("lockDevice.lockDeviceId", lockActivityCheckEvent.LockDeviceId);
                var lastEvent = events.OrderByDescending(a => a.EventTime).FirstOrDefault();
                if (lastEvent != null && lastEvent.EventTime <= lockActivityCheckEvent.EventDate)
                {
                    var lockDeviceList = await MongoDriver.MongoDbRepo.GetLockDeviceBsonsByField("lockDeviceId", lockActivityCheckEvent.LockDeviceId);
                    var lockDevice = lockDeviceList.FirstOrDefault();
                    if (lockDevice != null)
                    {
                        lockDevice.IsActive = false;
                        await MongoDriver.MongoDbRepo.UpsertLockDeviceBson(lockDevice);
                    }
                    else
                    {
                        //todo some error reportng
                    }

                }
                else
                {
                    //a newer event processed discard this check
                }
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine($"Activity check error for lockActivityCheckEvent, {e}");

            }

            return false;
        }

        public async Task<bool> ProcessEvent(byte[] data)
        {
            string jsonEvent = Encoding.UTF8.GetString(data);

            LockActivityCheck lockActivityCheck = JsonConvert.DeserializeObject<LockActivityCheck>(jsonEvent);
            return await LockEventProcessHandler.Invoke(lockActivityCheck);
        }
    }
}
