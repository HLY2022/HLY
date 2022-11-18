using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HLY.WEB.Data.Module
{
    public class BaseEntity
    {
        //GUID
        [Key,Column("GUID"),MaxLength(50)]
        public string Guid { get; set; }
        //操作员ID
        [Column("USERID"), MaxLength(50)]
        public string UserId { get; set; }
        //创建时间
        [Column("UPDATEDAT")]
        public DateTime UpdatedAt { get; set; }
        //修改时间
        [Column("CREATEDAT")]
        public DateTime CreatedAt { get; set; }
    }
}
