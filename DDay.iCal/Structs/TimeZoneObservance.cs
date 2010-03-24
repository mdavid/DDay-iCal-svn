﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DDay.iCal
{
#if DATACONTRACT
    [DataContract(Name = "TimeZoneObservance", Namespace = "http://www.ddaysoftware.com/dday.ical/2009/07/")]
#endif
    [Serializable]
    public struct TimeZoneObservance
    {
        public IPeriod Period { get; set; }
        public ITimeZoneInfo TimeZoneInfo { get; set; }

        public TimeZoneObservance(IPeriod period, ITimeZoneInfo tzi) : this()
        {
            Period = period;
            TimeZoneInfo = tzi;
        }
    }
}
