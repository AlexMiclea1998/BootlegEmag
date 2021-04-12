using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.ShoppingCart
{
    public class DatabaseConfiguration
    {
        public string ShoppingCartConnectionString { get; set; }

        public string EventConnectionString { get; set; }

        public string UserConnectionString { get; set; }

        public string ProductConnectionString { get; set; }
    }
}
