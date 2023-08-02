using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.ServiceHandler
{
    /// <summary>
    /// constants for use
    /// </summary>
    internal static class Constants
    {
        public const string ADD = "Add";
        public const string UPDATE = "Update";
        public const string GET = "Get";
        public const string TRANSACTIONREPORT = "TREPORT";
        public const string TRANSACTIONADJUSTREPORT = "TAREPORT";
    }

    internal static class Config
    {
        internal static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
