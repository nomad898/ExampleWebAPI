using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Business.Core.DTOs;
using Example.Business.Core.Interfaces;
using Example.Database.EF.Context.Configurations;
using Example.Database.EF.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Example.Business.Core.DTOs.Enums;

namespace Example.Business.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext; 
        public UserService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }
        public async Task<IEnumerable<UserDTO>> GetManyAsync()
        {
            using (_databaseContext)
            {
                var users = await _databaseContext.Users.ToListAsync();
                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            using (_databaseContext)
            {
                var user = await _databaseContext.Users.FindAsync(id);
                if (user != null)
                {
                    return _mapper.Map<UserDTO>(user);
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<UserDTO> GetAsync(string login)
        {
            using (_databaseContext)
            {
                var user = await _databaseContext.Users
                    .FirstOrDefaultAsync(_ => _.Login == login);
                if (user != null)
                {
                    return _mapper.Map<UserDTO>(user);
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<bool> CreateAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            using (_databaseContext)
            {
                await _databaseContext.AddAsync(user);
                var result = await _databaseContext.SaveChangesAsync();
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            using (_databaseContext)
            {
                _databaseContext.Attach(user);
                user.UserStateId = (int)UserStateEnum.Blocked;
                var result = await _databaseContext.SaveChangesAsync();
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (_databaseContext)
            {
                var user = await _databaseContext.Users.FindAsync(id);
                user.UserStateId = (int)UserStateEnum.Blocked;
                var result = await _databaseContext.SaveChangesAsync();
                return result > 0;
            }
        }
    }
}
