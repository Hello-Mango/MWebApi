using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.DTOS.Request
{
    public class UserReq
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string No { get; set; }

        public string Mobile { get; set; }
    }
}
