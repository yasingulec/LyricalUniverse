using LyricalUniverse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
