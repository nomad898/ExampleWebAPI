using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Business.Core.DTOs;
using Example.Business.Core.Interfaces;
using Example.Database.EF.Context.Configurations;

namespace Example.Business.Core.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _databaseContext; 
        public UserService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return null;
        }
    }
}
