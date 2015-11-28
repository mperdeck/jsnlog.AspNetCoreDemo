using Microsoft.Framework.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite
{
    public class Constants
    {
        private IApplicationEnvironment _appEnv;

        public Constants(IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;
        }

        public string LogFilePath
        {
            get { return Path.Combine(_appEnv.ApplicationBasePath, "Logs/log.txt"); }
        }
    }
}
