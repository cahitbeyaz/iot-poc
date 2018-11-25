using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockCommons.DB;
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
    }
}
