using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerDAL.Setting;
using LayerDAL.Entities;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace LayerDAL.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ConnectionSetting _connection;

        public UserRepository(IOptions <ConnectionSetting> connection) 
        {

            _connection = connection.Value;
        
        }


        public async Task<List<UserInfo>> GetUserInfos()
        {
            List<UserInfo> ListUsers = new List<UserInfo>();

            using (var connect = new SqlConnection(_connection.SqlString)) 
            {

                connect.Open();
                SqlCommand cmd = new SqlCommand("procedure_getUser", connect);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync()) 
                {
                
                    while(await reader.ReadAsync()) 
                    {

                        ListUsers.Add(new UserInfo()
                        {

                            ID = Convert.ToInt32(reader["ID"]),
                            Username = reader["Username"].ToString(),
                            passwordHash = (byte[])reader["passwordHash"],
                            passwordSalt = (byte[])reader["passwordSalt"]


                        });
                    
                    }
                
                }

            }

            return ListUsers;
        }
    }
}
