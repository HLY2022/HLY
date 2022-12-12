using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HLY.App.Request;
using HLY.App.Response;
using HLY.Repository;
using HLY.Repository.Domain;
using HLY.Repository.Interface;


namespace HLY.App
{
    public class SysLogApp : BaseStringApp<SysLog,HLYDBContext>
    {

        /// <summary>
        /// 加载列表
        /// </summary>
        public async Task<TableData> Load(QuerySysLogListReq request)
        {
            var result = new TableData();
            var objs = UnitWork.Find<SysLog>(null);
            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.Content.Contains(request.key) || u.Id.Contains(request.key));
            }

            result.data = await objs.OrderByDescending(u => u.CreateTime)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).ToListAsync();
            result.count = await objs.CountAsync();
            return result;
        }

        public void Add(SysLog obj)
        {
            //程序类型取入口应用的名称，可以根据自己需要调整
            obj.Application = Assembly.GetEntryAssembly().FullName.Split(',')[0];
            Repository.Add(obj);
        }
        
        public void Update(SysLog obj)
        {
            UnitWork.Update<SysLog>(u => u.Id == obj.Id, u => new SysLog
            {
               //todo:要修改的字段赋值
            });

        }

        public SysLogApp(IUnitWork<HLYDBContext> unitWork, IRepository<SysLog,HLYDBContext> repository) : base(unitWork, repository, null)
        {
        }
    }
}