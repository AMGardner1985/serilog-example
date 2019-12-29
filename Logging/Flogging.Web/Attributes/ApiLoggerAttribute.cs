using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Flogging.Core;
using Logging.Web;

namespace Flogging.Web.Attributes
{
    public class ApiLoggerAttribute : ActionFilterAttribute
    {
        private string _productName;

        public ApiLoggerAttribute(string productName)
        {                   
            _productName = productName;
        }        

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var dict = new Dictionary<string, object>();

            string userId, userName;
            var user = actionContext.RequestContext.Principal as ClaimsPrincipal;
            Helpers.GetUserData(dict, user, out userId, out userName);

            string location;
            Helpers.GetLocationForApiCall(actionContext.RequestContext, dict, out location);

            var qs = actionContext.Request.GetQueryNameValuePairs()
                        .ToDictionary(kv => kv.Key, kv => (object)kv.Value,
                            StringComparer.OrdinalIgnoreCase);

            var i = 0;
            foreach (var q in qs) 
            {
                    var newKey = string.Format("q-{0}-{1}", i++, q.Key);
                    if (!dict.ContainsKey(newKey))
                        dict.Add(newKey, q.Value);
            }

            var referrer = actionContext.Request.Headers.Referrer;
            if (referrer != null)
            {
                var source = actionContext.Request.Headers.Referrer.OriginalString;
                if (source.ToLower().Contains("swagger"))
                    source = "Swagger";
                if (!dict.ContainsKey("Referrer"))
                    dict.Add("Referrer", source);
            }

            actionContext.Request.Properties["PerfTracker"] = new PerfTracker(location, 
                userId, userName, location, _productName, "API", dict);
        }        

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                var perfTracker = actionExecutedContext.Request.Properties["PerfTracker"] 
                    as PerfTracker;

                if (perfTracker != null)
                    perfTracker.Stop();
            }
            catch (Exception)
            {
                // ignoring logging exceptions -- don't want an API call to fail 
                // if we run into logging problems. 
            }
        }                
    }
}
