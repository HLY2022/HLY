﻿using System;
using System.Collections.Generic;
using System.Linq;
 using System.Threading.Tasks;
 using Infrastructure;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
using HLY.App;
using HLY.App.Interface;
using HLY.App.Request;
using HLY.Repository.Domain;

namespace HLY.Mvc.Controllers
{
    public class OpenJobsController : BaseController
    {
        private readonly OpenJobApp _app;

         public OpenJobsController(OpenJobApp app, IAuth auth) : base(auth)
        {
            _app = app;
        }

        //主页
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //添加或修改
        [HttpPost]
        public string Add(AddOrUpdateOpenJobReq obj)
        {
            try
            {
                _app.Add(obj);
            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        //添加或修改
        [HttpPost]
        public string Update(AddOrUpdateOpenJobReq obj)
        {
            try
            {
                _app.Update(obj);

            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }
        
        /// <summary>
        /// 获取本地可执行的任务列表
        /// </summary>
        [HttpPost]
        public string QueryLocalHandlers()
        {
            var result = new Response<List<string>>();
            try
            {
                result.Result = _app.QueryLocalHandlers();
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }
        
        /// <summary>
        /// 改变任务状态，启动/停止
        /// </summary>
        [HttpPost]
        public string ChangeStatus(ChangeJobStatusReq req)
        {
            var result = new Response();
            try
            {
                _app.ChangeJobStatus(req);
                result.Message = "切换成功，可以在系统日志中查看运行结果";

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }
        

        /// <summary>
        /// 加载列表
        /// </summary>
        public async Task<string> Load([FromQuery]QueryOpenJobListReq request)
        {
            var objs = await _app.Load(request);
            return JsonHelper.Instance.Serialize(objs);
        }

        [HttpPost]
        public string Delete(string[] ids)
        {
            try
            {
                _app.Delete(ids);
            }
            catch (Exception e)
            {
                Result.Code = 500;
                Result.Message = e.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }
    }
}