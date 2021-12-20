using Microsoft.AspNetCore.Identity;
using MusicHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Identity
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        
    }
}
