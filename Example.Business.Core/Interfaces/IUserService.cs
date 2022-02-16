using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Business.Core.DTOs;

namespace Example.Business.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetManyAsync();
        Task<UserDTO> GetAsync(int id);
        Task<UserDTO> GetAsync(string login);
        Task<bool> CreateAsync(UserDTO user);
        Task<bool> DeleteAsync(UserDTO userDTO);
        Task<bool> DeleteAsync(int id);
    }
}
