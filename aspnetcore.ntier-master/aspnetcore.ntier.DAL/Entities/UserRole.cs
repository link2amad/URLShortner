using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspnetcore.ntier.DAL.Entities
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public string Role { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
