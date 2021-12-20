using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Models
{
    public class ProjectPermissions
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public bool Private { get; set; }
        public bool FriendsAllowed { get; set; }
    }
}
