using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusqvarnaTest.Models
{
    class CompanyModel
    {
        public string Name { get; set; } = string.Empty;
        public string OrganizationNumber { get; set; } = string.Empty;
        public int Employees { get; set; }
    }
}
