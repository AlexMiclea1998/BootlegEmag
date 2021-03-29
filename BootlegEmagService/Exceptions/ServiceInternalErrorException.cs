using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Exceptions
{
    public class ServiceInternalErrorException : Exception
    {
        public string ErrorMessage { get; set; }
    }
}
