using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Core
{
    public class PerfTracker
    {
        private readonly Stopwatch _stopWatch;

        private readonly LogDetail _infoToLog;

        public PerfTracker(string name, string userId, string userName, 
            string location, string product, string layer)
        {
            _stopWatch = Stopwatch.StartNew();
            _infoToLog = new LogDetail()
            {
                Message = name,
                UserId = userId,
                UserName = userName,
                Product = product,
                Layer = layer,
                Location = location,
                Hostname = Environment.MachineName
            };

            var beginTime = DateTime.Now;
            _infoToLog.AdditionalInfo = new Dictionary<string, object>() {
                { "Started", beginTime.ToString(CultureInfo.InvariantCulture)}
            };
        }

        /// <summary>
        /// Performance tracker constructure that will parse out additional parameters that debugging could find useful
        /// Will call base perf tracker constructure but looks through additional parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="location"></param>
        /// <param name="product"></param>
        /// <param name="layer"></param>
        /// <param name="perfParams"></param>
        public PerfTracker(string name, string userId, string userName,
                    string location, string product, string layer,
                    Dictionary<string, object> perfParams)
            : this(name, userId, userName, location, product, layer)
        {
            foreach (var item in perfParams)
            {
                _infoToLog.AdditionalInfo.Add("input-" + item.Key, item.Value);
            }
        }

        public void Stop()
        {
            _stopWatch.Stop();
            _infoToLog.ElapsedMilliseconds = _stopWatch.ElapsedMilliseconds;
            Logger.WritePerf(_infoToLog);
        }

    }
}
