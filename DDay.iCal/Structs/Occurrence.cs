using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace DDay.iCal
{    
#if !SILVERLIGHT
    [Serializable]
#endif
    public struct Occurrence :
        IComparable<Occurrence>
    {
        #region Private Fields
        
        private IPeriod m_Period;
        private IRecurrable m_Source; 

        #endregion

        #region Public Properties

        public IPeriod Period
        {
            get { return m_Period; }
            set { m_Period = value; }
        }

        public IRecurrable Source
        {
            get { return m_Source; }
            set { m_Source = value; }
        } 

        #endregion

        #region Constructors

        public Occurrence(Occurrence ao)
        {
            m_Period = ao.Period;
            m_Source = ao.Source;
        }

        public Occurrence(IRecurrable recurrable, IPeriod period)
        {
            m_Source = recurrable;
            m_Period = period;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj is Occurrence)
            {
                Occurrence o = (Occurrence)obj;
                return 
                    object.Equals(Source, o.Source) &&
                    CompareTo(o) == 0;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            string s = "Occurrence";
            if (Source != null)
                s = Source.GetType().Name + " ";

            if (Period != null)
                s += "(" + Period.StartTime.ToString() + ")";

            return s;
        }

        #endregion

        #region IComparable<Occurrence> Members

        public int CompareTo(Occurrence other)
        {
            return Period.CompareTo(other.Period);
        }

        #endregion
    }
}
