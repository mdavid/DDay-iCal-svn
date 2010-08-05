﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DDay.iCal
{
    /// <summary>
    /// An abstract class from which all iCalendar data types inherit.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class EncodableDataType :
        CalendarDataType,
        IEncodableDataType
    {
        #region IEncodableDataType Members

        virtual public string Encoding
        {
            get { return Parameters.Get("ENCODING"); }
            set { Parameters.Set("ENCODING", value); }
        }

        #endregion
    }
}
