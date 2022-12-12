// ***********************************************************************
// <summary>
// 超级管理员授权策略
// </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using HLY.App.Response;
using HLY.Repository;
using HLY.Repository.Domain;
using HLY.Repository.Interface;

namespace HLY.App
{
    /// <summary>
    /// 领域服务
    /// <para>超级管理员权限</para>
    /// <para>超级管理员使用guid.empty为ID，可以根据需要修改</para>
    /// </summary>
    public class SystemAuthStrategy : BaseStringApp<User,HLYDBContext>, IAuthStrategy
    {
        protected User _user;
        private DbExtension _dbExtension;

        public List<ModuleView> Modules
        {
            get {
                var modules = (from module in UnitWork.Find<Module>(null)
                    select new ModuleView
                    {
                        SortNo = module.SortNo,
                        Name = module.Name,
                        Id = module.Id,
                        CascadeId = module.CascadeId,
                        Code = module.Code,
                        IconName = module.IconName,
                        Url = module.Url,
                        ParentId = module.ParentId,
                        ParentName = module.ParentName,
                        IsSys = module.IsSys,
                        Status = module.Status
                    }).ToList();

                foreach (var module in modules)
                {
                    module.Elements = UnitWork.Find<ModuleElement>(u => u.ModuleId == module.Id).ToList();
                }

                return modules;
            }
        }

        public List<Role> Roles
        {
            get { return UnitWork.Find<Role>(null).ToList(); }
        }

        public List<ModuleElement> ModuleElements
        {
            get { return UnitWork.Find<ModuleElement>(null).ToList(); }
        }

        public List<Resource> Resources
        {
            get { return UnitWork.Find<Resource>(null).ToList(); }
        }

        public List<SysOrg> Orgs
        {
            get { return UnitWork.Find<SysOrg>(null).ToList(); }
        }

        public User User
        {
            get { return _user; }
            set   //禁止外部设置
            {
                throw new Exception("超级管理员，禁止设置用户");
            }  
        }
        

        public List<BuilderTableColumn> GetTableColumns(string moduleCode)
        {
            return UnitWork.Find<BuilderTableColumn>(u => u.TableName.ToLower() == moduleCode.ToLower()).ToList();
        }

        public List<BuilderTableColumn> GetTableColumnsFromDb(string moduleCode)
        {
            return _dbExtension.GetTableColumnsFromDb(moduleCode);
        }


        public SystemAuthStrategy(IUnitWork<HLYDBContext> unitWork, IRepository<User,HLYDBContext> repository, DbExtension dbExtension) : base(unitWork, repository, null)
        {
            _dbExtension = dbExtension;
            _user = new User
            {
                Account = Define.SYSTEM_USERNAME,
                Name = "超级管理员",
                Id = Guid.Empty.ToString()
            };
        }
    }
}