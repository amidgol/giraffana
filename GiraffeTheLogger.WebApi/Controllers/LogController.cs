using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiraffeTheLogger.ServiceContract;
using GiraffeTheLogger.Domain.Dtos;
using GiraffeTheLogger.Domain.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GiraffeTheLogger.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class LogController : ControllerBase
    {
        private readonly IQueueService queueService;
        private readonly IOptions<QueueOptions> queueOptions;

        public LogController(IQueueService queueService, IOptions<QueueOptions> queueOptions)
        {
            this.queueService = queueService;
            this.queueOptions = queueOptions;
        }
        
        [HttpPost]
        public IActionResult Add([FromBody] MessageDto messageDto)
        {
            queueService.Enqueue(messageDto, queueOptions.Value.QueueName);

            return NoContent();
        }
    }
}
