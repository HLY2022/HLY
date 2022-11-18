using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HLY.WEB.Data.Module
{
    [Table("USERS")]
    public class Users:BaseEntity
    {
        //机构ID
         [Column("ORGID"), MaxLength(50)]
        public string OrgId { get; set; }
        //用户编码
         [Column("CODE"), MaxLength(50)]
        public string Code { get; set; }
        //用户名称
         [Column("NAME"), MaxLength(200)]
        public string Name { get; set; }
        //邮箱
         [Column("EMAIL"), MaxLength(200)]
        public string Email { get; set; }
        //图片
        [Column("PROFILEIMAGE"), MaxLength(500)]
        public string ProfileImage { get; set; }
        //密码
        [Column("PASSWORDHASH")]
        public string PasswordHash { get; set; }
        //权限组
        [Column("GROUPS"), MaxLength(2000)]
        public string Groups { get; set; }
        //共享KEY
        [Column("APIKEY"), MaxLength(200)]
        public string ApiKey { get; set; }
        //是否启用
        [Column("DISABLED")]
        public int Disabled { get; set; }
        //手机号
        [Column("MOBILE"), MaxLength(50)]
        public string Mobile { get; set; }
        //部门ID
        [Column("ORGUNITID"), MaxLength(50)]
        public string OrgunitId { get; set; }
    }
}