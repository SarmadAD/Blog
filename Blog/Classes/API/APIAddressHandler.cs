using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Classes.API
{
    public class APIAddressHandler
    {
#if DEBUG
        public static string APIAddress = "https://localhost:44375/api/";
#else
        public static string APIAddress = "RELEASEADDRESS";
    }
#endif
    }
}
