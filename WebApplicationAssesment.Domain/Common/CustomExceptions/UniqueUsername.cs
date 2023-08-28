﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationAssesment.Domain.Common.CustomExceptions
{
    public class UniqueUsername : Exception
    {
        public UniqueUsername(string message) : base(message) { }
    }
}
