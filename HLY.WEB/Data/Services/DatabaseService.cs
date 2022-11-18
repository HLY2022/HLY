using HLY.WEB.Data.IServices;

namespace HLY.WEB.Data.Services
{
    public class DatabaseService:IDatabaseService
    {
        private DataContext _dataContext;

        public DatabaseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateDatabase()
        {
            //查询数据库中的信息
            _dataContext.Database.EnsureCreated();
        }
    }
}
