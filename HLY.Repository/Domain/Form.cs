﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using HLY.Repository.Core;

namespace HLY.Repository.Domain
{
    /// <summary>
	/// 表单模板表
	/// </summary>
      [Table("Form")]
    public partial class Form : StringEntity
    {
        public Form()
        {
          this.Name= string.Empty;
          this.FrmType= 0;
          this.WebId= string.Empty;
          this.Fields= 0;
          this.ContentData= string.Empty;
          this.ContentParse= string.Empty;
          this.Content= string.Empty;
          this.SortCode= 0;
          this.DeleteMark= 0;
          this.DbName= string.Empty;
          this.Disabled= 0;
          this.Description= string.Empty;
          this.CreateDate= DateTime.Now;
          this.CreateUserId= string.Empty;
          this.CreateUserName= string.Empty;
          this.ModifyDate= DateTime.Now;
          this.ModifyUserId= string.Empty;
          this.ModifyUserName= string.Empty;
        }

        /// <summary>
	    /// 表单名称
	    /// </summary>
         [Description("表单名称")]
        public string Name { get; set; }
        /// <summary>
	    /// 表单类型，0：默认动态表单；1：Web自定义表单
	    /// </summary>
         [Description("表单类型，0：默认动态表单；1：Web自定义表单")]
        public int FrmType { get; set; }
        /// <summary>
	    /// 系统页面标识，当表单类型为用Web自定义的表单时，需要标识加载哪个页面
	    /// </summary>
         [Description("系统页面标识，当表单类型为用Web自定义的表单时，需要标识加载哪个页面")]
        public string WebId { get; set; }
        /// <summary>
	    /// 字段个数
	    /// </summary>
         [Description("字段个数")]
        public int Fields { get; set; }
        /// <summary>
	    /// 表单中的控件属性描述
	    /// </summary>
         [Description("表单中的控件属性描述")]
        public string ContentData { get; set; }
        /// <summary>
	    /// 表单控件位置模板
	    /// </summary>
         [Description("表单控件位置模板")]
        public string ContentParse { get; set; }
        /// <summary>
	    /// 表单原html模板未经处理的
	    /// </summary>
         [Description("表单原html模板未经处理的")]
        public string Content { get; set; }
        /// <summary>
	    /// 排序码
	    /// </summary>
         [Description("排序码")]
        public int SortCode { get; set; }
        /// <summary>
	    /// 删除标记
	    /// </summary>
         [Description("删除标记")]
        public int DeleteMark { get; set; }
        /// <summary>
	    /// 数据库名称
	    /// </summary>
         [Description("数据库名称")]
        public string DbName { get; set; }
        /// <summary>
	    /// 有效
	    /// </summary>
         [Description("有效")]
        public int Disabled { get; set; }
        /// <summary>
	    /// 备注
	    /// </summary>
         [Description("备注")]
        public string Description { get; set; }
        /// <summary>
	    /// 创建时间
	    /// </summary>
         [Description("创建时间")]
        public System.DateTime CreateDate { get; set; }
        /// <summary>
	    /// 创建用户主键
	    /// </summary>
         [Description("创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
	    /// 创建用户
	    /// </summary>
         [Description("创建用户")]
        public string CreateUserName { get; set; }
        /// <summary>
	    /// 修改时间
	    /// </summary>
         [Description("修改时间")]
        public System.DateTime? ModifyDate { get; set; }
        /// <summary>
	    /// 修改用户主键
	    /// </summary>
         [Description("修改用户主键")]
        public string ModifyUserId { get; set; }
        /// <summary>
	    /// 修改用户
	    /// </summary>
         [Description("修改用户")]
        public string ModifyUserName { get; set; }
        
        /// <summary>
        /// 所属部门
        /// </summary>
        [Description("所属部门")]
        public string OrgId { get; set; }

    }
}