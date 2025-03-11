using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs
{

    public class UpdateRoleRequest
    {
        public string OldRoleName { get; set; }
        public string NewRoleName { get; set; }
    }


}
