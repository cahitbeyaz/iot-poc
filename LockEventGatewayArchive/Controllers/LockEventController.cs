using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LockCommons.Models;
using LockCommons.Mq;
using Microsoft.AspNetCore.Mvc;

namespace LockEventGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockEventController : ControllerBase
    {

        [HttpPost]
        public LockEventResult Post([FromBody] LockEventRequest lockEvent)
        {
            EventMqBroker.Queue(lockEvent);
            return new LockEventResult { Result = new BaseResult(0, "success") };
        }


    }
}
