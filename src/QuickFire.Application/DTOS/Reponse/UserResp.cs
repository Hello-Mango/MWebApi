using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickFire.Application.DTOS.Reponse
{
    public class UserResp
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsLock { get; set; }
        public List<string> Role { get; set; }
    }
}
