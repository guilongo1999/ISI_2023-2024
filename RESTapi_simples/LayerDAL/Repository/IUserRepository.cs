using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerDAL.Entities; //para a pasta entities


namespace LayerDAL.Repository
{
    public interface IUserRepository
    {

        Task<List<UserInfo>> GetUserInfos();


    }
}
