using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySession2207.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public DateTime BDate { get; set; }
    }
}
