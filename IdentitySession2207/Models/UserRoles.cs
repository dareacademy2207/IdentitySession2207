using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySession2207.Models
{
    public class UserRoles
    {
        public string RoleName { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool IsSelected { get; set; }
    }
}
