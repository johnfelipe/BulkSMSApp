using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Helpers
{
    public class Alert
    {
        public const string TempDataKey = "TempDataAlerts";

        public string alertIcon { get; set; }
        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
    }
}