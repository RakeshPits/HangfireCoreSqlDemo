using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HangfireDemoForAll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : ControllerBase
    {      
        [HttpPost]
        [Route("[action]")]
        public IActionResult WelcomeMail()
        {
            var jobId = BackgroundJob.Enqueue(() => SendWelcomeMail("Welcome To Ootty Nice to Meet You...!!"));
            return Ok($"Job Id : {jobId},welcome mail send");
        }

        public void SendWelcomeMail(string message)
        {
            Console.WriteLine(message);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult DiscountMail()
        {
            var jobId = BackgroundJob.Schedule(() => SendDiscountMail("Welcome To Ootty Nice to Meet You...!!"), TimeSpan.FromSeconds(45));
            return Ok($"Job Id : {jobId}, Send discount mail to customer");
        }

        public void SendDiscountMail(string message)
        {
            Console.WriteLine(message);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult DatabaseCheck()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Check data base"), Cron.Minutely);
            return Ok($"Check Data base Minutely");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Notification()
        {
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Check, is status updated"), TimeSpan.FromSeconds(50));
            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Status Updated"));
            return Ok($"Notifaction updated");
        }
    }
}
