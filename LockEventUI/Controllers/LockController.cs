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
        public async Task UpdateLockStatus(ApiLockDevice lockDevice)
        {
            LockDeviceBson lockDeviceBson = new LockDeviceBson()
            {
                LockDeviceId = lockDevice.LockDeviceId,
                IsActive = lockDevice.IsActive
            };

            await MongoDriver.MongoDbRepo.UpsertLockDeviceBson(lockDeviceBson);
        }
    }
}
