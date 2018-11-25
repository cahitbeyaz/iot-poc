using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LockCommons.DB;
using LockCommons.Models;
using LockCommons.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace LockEventUI.Controllers
{
    [Route("api/[controller]")]
    public class LockController : Controller
    {

        [HttpGet("[action]")]
        public IEnumerable<LockDeviceEventReportModel> LockEvents()
        {
            return MongoDriver.MongoDbRepo.GetLockDevicesEvents();
        }

        [HttpPost("[action]")]
        public async Task UpdateLockStatus([FromBody]ApiLockDevice lockDevice)
        {

            var ld = await MongoDriver.MongoDbRepo.GetLockDeviceBsonsByField("lockDeviceId", lockDevice.LockDeviceId);

            if (ld.FirstOrDefault() != null)
            {
                LockDeviceBson lockDeviceBson = ld.FirstOrDefault();
                lockDeviceBson.IsActive = lockDevice.IsActive;
                await MongoDriver.MongoDbRepo.UpsertLockDeviceBson(lockDeviceBson);
            }
            else
                throw new Exception($"Lock device not found {lockDevice}");

        }
    }
}
