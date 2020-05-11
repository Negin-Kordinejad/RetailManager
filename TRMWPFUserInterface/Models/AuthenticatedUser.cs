using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace TRMWPFUserInterface.Models
{
    public class AuthenticatedUser
    {
        public string Access_Token { get; set; }
        public string UserName { get; set; }
    }
}
