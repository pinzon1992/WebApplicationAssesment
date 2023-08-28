using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAssesment.Domain.Common.CustomExceptions
{
    public class InvalidCredentials : Exception
    {
        public InvalidCredentials(string Message) : base(message: Message)
        {
            
        }
    }
}
