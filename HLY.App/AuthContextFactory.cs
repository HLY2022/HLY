
// ***********************************************************************
// <summary>
// 用户权限策略工厂
//</summary>
// ***********************************************************************

using Infrastructure;
using HLY.Repository;
using HLY.Repository.Domain;
using HLY.Repository.Interface;

namespace HLY.App
{
    /// <summary>
    ///  加载用户所有可访问的资源/机构/模块
    /// </summary>
    public class AuthContextFactory
    {
        private SystemAuthStrategy _systemAuth;
        private NormalAuthStrategy _normalAuthStrategy;
        private readonly IUnitWork<HLYDBContext> _unitWork;

        public AuthContextFactory(SystemAuthStrategy sysStrategy
            , NormalAuthStrategy normalAuthStrategy
            , IUnitWork<HLYDBContext> unitWork)
        {
            _systemAuth = sysStrategy;
            _normalAuthStrategy = normalAuthStrategy;
            _unitWork = unitWork;
        }

        public AuthStrategyContext GetAuthStrategyContext(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;

            IAuthStrategy service = null;
             if (username == Define.SYSTEM_USERNAME)
            {
                service= _systemAuth;
            }
            else
            {
                service = _normalAuthStrategy;
                service.User = _unitWork.FirstOrDefault<User>(u => u.Account == username);
            }

         return new AuthStrategyContext(service);
        }
    }
}