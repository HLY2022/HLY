using HLY.WEB.Data.Module;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HLY.WEB.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //判断当前数据库是Oracle
            if (Database.IsOracle())
            {   //如果需要，手动添加Schema名称，如果是默认或者表前不需要Schema名就可以不用配置
                //modelBuilder.HasDefaultSchema("EVMS");//如果使用Oracle必须手动添加Schema，判断当前数据库是Oracle 需要手动添加Schema(DBA提供的数据库账号名称)

                //如果使用Oracle必须手动添加Schema，判断当前数据库是Oracle 需要手动添加Schema(DBA提供的数据库账号名称)
                modelBuilder.HasDefaultSchema("GADATAHLY");//注意：DBA提供的数据库账号名称，必须大写
            }
            //此方法可以将当前程序集下所有继承了ComplexTypeConfiguration、EntityTypeConfiguration类型的类添加到注册器
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Users>(entity =>
            //{
            //    entity.ToTable("USERS");
            //    entity.Property(e => e.Guid).IsRequired();
            //    entity.Property(e => e.Guid).HasColumnName("GUID");
            //    entity.Property(e => e.OrgId).HasColumnName("ORGID");
            //    entity.Property(e => e.Code).HasColumnName("CODE");
            //    entity.Property(e => e.Name).HasColumnName("NAME");
            //    entity.Property(e => e.Email).HasColumnName("EMAIL");
            //    entity.Property(e => e.ProfileImage).HasColumnName("PROFILEIMAGE");

            //    entity.Property(e => e.PasswordHash).HasColumnName("PASSWORDHASH");
            //    entity.Property(e => e.Groups).HasColumnName("GROUPS");
            //    entity.Property(e => e.ApiKey).HasColumnName("APIKEY");
            //    entity.Property(e => e.Disabled).HasColumnName("DISABLED");

            //    entity.Property(e => e.Mobile).HasColumnName("MOBILE");
            //    entity.Property(e => e.OrgunitId).HasColumnName("ORGUNITID");
            //    entity.Property(e => e.UserId).HasColumnName("USERID");
            //    entity.Property(e => e.UpdatedAt).HasColumnName("UPDATEDAT");
            //    entity.Property(e => e.CreatedAt).HasColumnName("CREATEDAT");
            //});
        }
        public DbSet<Users> Users { get; set; }
    }
}
