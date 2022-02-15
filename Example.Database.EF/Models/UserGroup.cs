using System.Collections.Generic;

namespace Example.Database.EF.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
