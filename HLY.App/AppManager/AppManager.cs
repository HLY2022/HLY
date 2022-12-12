using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HLY.App.Interface;
using HLY.App.Request;
using HLY.Repository;
using HLY.Repository.Domain;
using HLY.Repository.Interface;

namespace HLY.App
{
    /// <summary>
    /// 分类管理
    /// </summary>
    public class AppManager : BaseStringApp<Application, HLYDBContext>
    {
        public AppManager(IUnitWork<HLYDBContext> unitWork, IRepository<Application, HLYDBContext> repository) : base(unitWork, repository, null)
        {
        }

        public void Add(Application Application)
        {
            if (string.IsNullOrEmpty(Application.Id))
            {
                Application.Id = Guid.NewGuid().ToString();
            }

            Repository.Add(Application);
        }

        public void Update(Application Application)
        {
            Repository.Update(Application);
        }


        public async Task<List<Application>> GetList(QueryAppListReq request)
        {
            var applications = UnitWork.Find<Application>(null);

            return await applications.ToListAsync();
        }


        public Application GetByAppKey(string modelAppKey)
        {
            return Repository.FirstOrDefault(u => u.AppSecret == modelAppKey);
        }
    }
}