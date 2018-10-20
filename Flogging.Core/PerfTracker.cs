﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace C3logging.Core
{
    public class PerfTracker
    {
        private readonly Stopwatch _sw;
        private readonly C3loglogDetail _infoToLog;

        public PerfTracker(string name, string userId, string userName,
                   string location, string product, string layer)
        {
            _sw = Stopwatch.StartNew();
            _infoToLog = new C3loglogDetail()
            {
                Message = name,
                UserName = userName,
                Timestamp = DateTime.Now,
                Product = product,
                Layer = layer,
                Location = location,
                Hostname = Environment.MachineName
            };

            var beginTime = DateTime.Now;
            _infoToLog.AdditionalInfo = new Dictionary<string, object>()
            {
                { "Started", beginTime.ToString(CultureInfo.InvariantCulture)   }
            };
        }

        public PerfTracker(string name, string userId, string userName,
                   string location, string product, string layer,
                   Dictionary<string, object> perfParams)
            : this(name, userId, userName, location, product, layer)
        {
            foreach (var item in perfParams)
                _infoToLog.AdditionalInfo.Add("input-" + item.Key, item.Value);
        }

        public void Stop()
        {
            _sw.Stop();
            _infoToLog.ElapsedMilliseconds = _sw.ElapsedMilliseconds;
            C3logger.WritePerf(_infoToLog);
        }
    }


}
