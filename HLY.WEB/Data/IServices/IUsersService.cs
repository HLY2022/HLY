using HLY.WEB.Data.Module;
using System.Collections.Generic;

namespace HLY.WEB.Data.IServices
{
    public interface IUsersService
    {

        Users Authenticate(string username, string password);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        Users Create(Users user, string password);
        void Update(Users user, string password = null);
        void Delete(int id);

    }
}
