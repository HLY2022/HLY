using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Const;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HLY.App.Extensions;
using HLY.App.Interface;
using HLY.App.Jobs;
using HLY.App.Request;
using HLY.App.Response;
using HLY.Repository;
using HLY.Repository.Domain;
using HLY.Repository.Interface;
using Quartz;


namespace HLY.App
{
    /// <summary>
    /// 系统定时任务管理
    /// </summary>
    public class OpenJobApp : BaseStringApp<OpenJob, HLYDBContext>
    {
        private SysLogApp _sysLogApp;
        private IScheduler _scheduler;
        private ILogger<OpenJobApp> _logger;

        /// <summary>
        /// 加载列表
        /// </summary>
        public async Task<TableData> Load(QueryOpenJobListReq request)
        {
            var result = new TableData();
            var objs = Repository.Find(null);
            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.Id.Contains(request.key));
            }

            result.data =await objs.OrderBy(u => u.Id)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).ToListAsync();
            result.count =await objs.CountAsync();
            return result;
        }

        /// <summary>
        /// 启动所有状态为正在运行的任务
        /// <para>通常应用在系统加载的时候</para>
        /// </summary>
        /// <returns></returns>
        public async Task StartAll()
        {
            var jobs = await Repository.Find(u => u.Status == (int) JobStatus.Running).ToListAsync();
            foreach (var job in jobs)
            {
                job.Start(_scheduler);
            }
            _logger.LogInformation("所有状态为正在运行的任务已启动");
        }

        public void Add(AddOrUpdateOpenJobReq req)
        {
            var obj = req.MapTo<OpenJob>();
            obj.CreateTime = DateTime.Now;
            var user = _auth.GetCurrentUser().User;
            obj.CreateUserId = user.Id;
            obj.CreateUserName = user.Name;
            Repository.Add(obj);
        }

        public void Update(AddOrUpdateOpenJobReq obj)
        {
            var user = _auth.GetCurrentUser().User;
            UnitWork.Update<OpenJob>(u => u.Id == obj.Id, u => new OpenJob
            {
                JobName = obj.JobName,
                JobType = obj.JobType,
                JobCall = obj.JobCall,
                JobCallParams = obj.JobCallParams,
                Cron = obj.Cron,
                Status = obj.Status,
                Remark = obj.Remark,
                UpdateTime = DateTime.Now,
                UpdateUserId = user.Id,
                UpdateUserName = user.Name
            });
        }

        #region 定时任务运行相关操作

        /// <summary>
        /// 返回系统的job接口
        /// </summary>
        /// <returns></returns>
        public List<string> QueryLocalHandlers()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces()
                    .Contains(typeof(IJob))))
                .ToArray();
            return types.Select(u => u.FullName).ToList();
        }

        public void ChangeJobStatus(ChangeJobStatusReq req)
        {
            var job = Repository.FirstOrDefault(u => u.Id == req.Id);
            if (job == null)
            {
                throw new Exception("任务不存在");
            }


            if (req.Status == (int) JobStatus.NotRun) //停止
            {
                job.Stop(_scheduler);
            }
            else //启动
            {
                job.Start(_scheduler);
            }


            var user = _auth.GetCurrentUser().User;

            job.Status = req.Status;
            job.UpdateTime = DateTime.Now;
            job.UpdateUserId = user.Id;
            job.UpdateUserName = user.Name;
            Repository.Update(job);
        }
        /// <summary>
        /// 记录任务运行结果
        /// </summary>
        /// <param name="jobId"></param>
        public void RecordRun(string jobId)
        {
            var job = Repository.FirstOrDefault(u => u.Id == jobId);
            if (job == null)
            {
                _sysLogApp.Add(new SysLog
                {
                    TypeName = "定时任务",
                    TypeId = "AUTOJOB",
                    Content = $"未能找到定时任务：{jobId}"
                });
                return;
            }

            job.RunCount++;
            job.LastRunTime = DateTime.Now;
            Repository.Update(job);

            _sysLogApp.Add(new SysLog
            {
                CreateName = "Quartz",
                CreateId = "Quartz",
                TypeName = "定时任务",
                TypeId = "AUTOJOB",
                Content = $"运行了自动任务：{job.JobName}"
            });
            _logger.LogInformation($"运行了自动任务：{job.JobName}");
        }

        #endregion


        public OpenJobApp(IUnitWork<HLYDBContext> unitWork, IRepository<OpenJob, HLYDBContext> repository,
            IAuth auth, SysLogApp sysLogApp, IScheduler scheduler, ILogger<OpenJobApp> logger) : base(unitWork,
            repository, auth)
        {
            _sysLogApp = sysLogApp;
            _scheduler = scheduler;
            _logger = logger;
        }
    }
}