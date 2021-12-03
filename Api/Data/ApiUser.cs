using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Api.Data
{
    public class ApiUser : IdentityUser
    {
        public string  FisrtName { get; set; }
        public string LastName { get; set; }
    }
}