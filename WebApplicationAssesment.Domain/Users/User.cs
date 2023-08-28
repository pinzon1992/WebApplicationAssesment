using WebApplicationAssesment.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAssesment.Domain.Users
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long AccountId { get; set; }
        public Account Account { get; set; }
    }
}
