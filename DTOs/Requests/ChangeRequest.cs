using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.DTOs.Requests
{
    public class ChangeRequest
    {
        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
