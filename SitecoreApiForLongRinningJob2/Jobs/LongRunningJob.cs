using Sitecore.Jobs;
using Sitecore;
using Sitecore.Data.Items;
using System.Threading.Tasks;
using System.Threading;

namespace SitecoreApiForLongRinningJob2.Jobs
{
    public class LongRunningJob
    {
        private string _jobName = "LongRunningJob";

        public LongRunningJob(string jobName)
        {
            _jobName = jobName;
        }

        public DefaultJob Job
        {
            get
            {
                return (DefaultJob)JobManager.GetJob(_jobName);
            }
        }

        public string StartJob(int delay)
        {
            DefaultJobOptions options = new DefaultJobOptions(_jobName, "MyCode", Context.Site.Name, this, "ConvertData", new object[] { delay });
            JobManager.Start(options);
            return _jobName;
        }

        public void ConvertData(int delay)
        {
            Sitecore.Diagnostics.Log.Info($"Starting {delay} sec. delay...", this);
            Thread.Sleep(delay);
            Sitecore.Diagnostics.Log.Info($"Done {delay} sec. delay...", this);
            if (Job != null)
            {
                Job.Status.State = JobState.Finished;
            }
        }
    }
}