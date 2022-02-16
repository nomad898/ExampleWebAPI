using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Business.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedTime { get; set; }
        public int UserGroupId { get; set; }
        public int UserStateId { get; set; }
    }
}
