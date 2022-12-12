﻿﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HLY.Repository.Core;

namespace HLY.App.Request
{
    /// <summary>
	/// 定时任务
	/// </summary>
    [Table("OpenJob")]
    public partial class AddOrUpdateOpenJobReq 
    {

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 任务执行方式0：本地任务；1：外部接口任务
        /// </summary>
        public int JobType { get; set; }
        /// <summary>
        /// 任务地址
        /// </summary>
        public string JobCall { get; set; }
        /// <summary>
        /// 任务参数，JSON格式
        /// </summary>
        public string JobCallParams { get; set; }
        /// <summary>
        /// CRON表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 任务运行状态（0：停止，1：正在运行，2：暂停）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}