using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Business.Core.DTOs;
using Example.Business.Core.Interfaces;
using Example.Web.API.Attributes.Security;
using Example.Business.Core.DTOs.Enums;
using Example.Web.API.Models;
using AutoMapper;

namespace Example.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            _logger.LogDebug("Fetching all users...");
            return await _userService.GetManyAsync();
        }

        [HttpGet("{id}")]
        public async Task<UserDTO> Get(int id)
        {
            _logger.LogDebug("Fetching all users...");
            return await _userService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserVM userVM)
        {
            var userDTO = _mapper.Map<UserDTO>(userVM);
            var isCreated = await _userService.CreateAsync(userDTO);
            if (isCreated)
            {
                return CreatedAtAction(nameof(Get), new { id = userDTO.Id }, userDTO);
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        //[Authorize(UserGroupEnum.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _userService.DeleteAsync(id);
            if (isDeleted)
            {
                return new OkResult();
            }
            return new NoContentResult();
        }
    }
}
