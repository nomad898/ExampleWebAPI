using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Business.Core.DTOs;

namespace Example.Business.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();
    }
}
