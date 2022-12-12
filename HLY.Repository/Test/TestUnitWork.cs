using System;
using System.Data;
using System.Linq;
using Infrastructure;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using HLY.Repository.Domain;
using HLY.Repository.Interface;

namespace HLY.Repository.Test
{
    /// <summary>
    /// 测试UnitWork
    /// </summary>
    class TestUnitWork : TestBase
    {
        /// <summary>
        /// 测试存储过程
        /// </summary>
        [Test]
        public void ExecProcedure()
        {
            var unitWork = _autofacServiceProvider.GetService<IUnitWork<HLYDBContext>>();
            var users = unitWork.ExecProcedure<User>("sp_alluser");
            Console.WriteLine(JsonHelper.Instance.Serialize(users));
        }
        
        
        /// <summary>
        /// 测试Mysql执行存储过程
        /// </summary>
        [Test]
        public void ExecProcedureWithParam()
        {
            var unitWork = _autofacServiceProvider.GetService<IUnitWork<HLYDBContext>>();
            var param = new MySqlParameter("keyword", SqlDbType.NVarChar);
            param.Value = "test%";
            var users = unitWork.ExecProcedure<User>("sp_alluser", new []{param});
            Console.WriteLine(JsonHelper.Instance.Serialize(users));
        }
        
    }
}