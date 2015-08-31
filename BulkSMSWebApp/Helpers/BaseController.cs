using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulkSMSWebApp.Helpers
{
    public class BaseController : Controller
    {
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable, "fa-check");
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable, "fa-check-circle");
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable, "fa-exclamation-circle");
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable, "fa-exclamation-triangle");
        }

        private void AddAlert(string alertStyle, string message, bool dismissable, string alertIcon)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                alertIcon = alertIcon
            });

            TempData[Alert.TempDataKey] = alerts;
        }


    }
}