using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace LoggerImplementation
{
    public class LoggerFactory
    {
        public ILogger GetLogger()
        {
            return new NLog.LogFactory().GetLogger("SocialNetworkLogger");
        }
    }
}
