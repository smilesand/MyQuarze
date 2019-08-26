using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeTask.SchedulingTasks
{
    public class Job : BaseJob, IJob
    {
        public Job()
        {
            TaskName = "TORCH";
        }
        public override void Execute(IJobExecutionContext context)
        {
            try
            {
                base.NextTime(context);
            }
            catch (Exception ex)
            {
                log.Error(ex, $"{this.TaskName}同步出错");
            }
        }

        /// <summary>
        /// 注册方法
        /// </summary>
        /// <param name="TaskName"></param>
        /// <param name="TaskGroup"></param>
        /// <returns></returns>
        public override IJobDetail CreateJobDetail(string TaskGroup)
        {
            IJobDetail simpleJob = JobBuilder.Create<Job>().WithIdentity(TaskName, TaskGroup).Build();
            return simpleJob;
        }

        /// <summary>
        /// 每个插件在此自定义触发器
        /// </summary>
        /// <param name="TriggerName"></param>
        /// <param name="TriggerGroup"></param>
        /// <returns></returns>
        public override ITrigger CreateITrigger(string TriggerName, string TriggerGroup)
        {
            cronTrigger = base.cronTrigger;
            return cronTrigger;
        }
    }
}
