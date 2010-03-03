﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DDay.iCal
{
    public interface IUTCOffset :
        ICalendarDataType
    {
        bool Positive { get; set; }
        int Hours { get; set; }
        int Minutes { get; set; }
        int Seconds { get; set; }
    }
}
