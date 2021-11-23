using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sitecore.Jobs;
using SitecoreApiForLongRinningJob2.Jobs;

namespace SitecoreApiForLongRinningJob2.Controllers
{
    public class LongRunningController : Controller
    {
        // GET: LongRunning
        public ActionResult Index()
        {            
            return Json(new {status = "OK"});
        }
        
        [HttpPost]
        public ActionResult Start(int delay)
        {
            var job = new LongRunningJob(Guid.NewGuid().ToString());
            var jobName = job.StartJob(delay);
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            return Json(new {message = "long running task created, use job name to request the status", jobName = jobName });
        }

        [HttpPost]
        public ActionResult JobStatus(string jobName)
        {
            var status = "JOB NOT FOUND";
            DefaultJob Job = (DefaultJob)JobManager.GetJob(jobName);
            
            if(Job != null)
            {
                status = Job.Status.State.ToString();
            }            

            return Json(new { status = status });
        }
    }
}