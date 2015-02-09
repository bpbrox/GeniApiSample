/****************************** Module Header ******************************\
Module Name:  UnixTimestamp.cs
Project:      GeniApiSample
Copyright (c) Bjørn P. Brox <bjorn.brox@gmail.com>

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Globalization;

namespace GeniApiSample
{
    /// <summary>
    /// The unix time stamp is a way to track time as a running total of seconds.
    /// This count starts at the Unix Epoch on January 1st, 1970.
    /// Therefore, the unix time stamp is merely the number of seconds between a
    /// particular date and the Unix Epoch.
    /// 
    /// This is very useful to computer systems for tracking and sorting dated information
    /// in dynamic and distributed applications both online and client side.
    /// 
    /// This implementation does not have the January 19, 2038 (32-bit overflow) limitation by using
    /// a double to store the value.
    /// </summary>
    public class UnixTimestamp : IComparable, IComparable<UnixTimestamp>, IEquatable<UnixTimestamp>, IFormattable
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        private double SecondsSinceUnixEpoch { get; set; }

        public bool HasValue { get { return SecondsSinceUnixEpoch > 0.0; } }
        public DateTime DateTime
        {
            get { return UnixEpoch.AddSeconds(SecondsSinceUnixEpoch); }
            set { SecondsSinceUnixEpoch = (value - UnixEpoch.ToLocalTime()).TotalSeconds; }
        }

        #region ctor
        public UnixTimestamp(double secondsSinceUnixEpoch)
        {
            SecondsSinceUnixEpoch = secondsSinceUnixEpoch;
        }

        public UnixTimestamp(long secondsSinceUnixEpoch)
        {
            SecondsSinceUnixEpoch = secondsSinceUnixEpoch;
        }

        public UnixTimestamp(DateTime dateTime)
            : this((dateTime - UnixEpoch.ToLocalTime()).TotalSeconds)
        {
        }
        #endregion

        #region IComparable
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            UnixTimestamp other = obj as UnixTimestamp;
            if (other != null)
                return CompareTo(other);
            throw new ArgumentException("Object is not a UnixTimestamp");
        }

        public int CompareTo(UnixTimestamp other)
        {
            return other == null ? 1 : SecondsSinceUnixEpoch.CompareTo(other.SecondsSinceUnixEpoch);
        }
        #endregion
        
        #region IEquatable<UnixTimestamp>
        public bool Equals(UnixTimestamp other)
        {
            return SecondsSinceUnixEpoch.Equals(other.SecondsSinceUnixEpoch);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(UnixTimestamp) && Equals((UnixTimestamp)obj);
        }

        public override int GetHashCode()
        {
            return SecondsSinceUnixEpoch.GetHashCode();
        }
        #endregion

        #region ToString()
        public string ToLongDateString()
        {
            return DateTime.ToLongDateString();
        }

        public String ToLongTimeString()
        {
            return DateTime.ToLongTimeString();
        }

        public string ToShortDateString()
        {
            return DateTime.ToShortDateString();
        }

        public String ToShortTimeString()
        {
            return DateTime.ToShortTimeString();
        }

        public override string ToString()
        {
            return DateTime.ToString(DateTimeFormatInfo.CurrentInfo);
        }

        public string ToString(string format)
        {
            return DateTime.ToString(format);
        }

        public string ToString(IFormatProvider provider)
        {
            return DateTime.ToString(provider);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return DateTime.ToString(format, provider);
        }
        #endregion

        #region Operators
        public static UnixTimestamp operator +(UnixTimestamp d, TimeSpan t)
        {
            return new UnixTimestamp(d.SecondsSinceUnixEpoch + t.TotalSeconds);
        }

        public static UnixTimestamp operator -(UnixTimestamp d, TimeSpan t)
        {
            return new UnixTimestamp(d.SecondsSinceUnixEpoch - t.TotalSeconds);
        }

        public static TimeSpan operator -(UnixTimestamp d1, UnixTimestamp d2)
        {
            return new TimeSpan(0, 0, 0, 0, (int)(1000.0*(d1.SecondsSinceUnixEpoch - d2.SecondsSinceUnixEpoch)));
        }
        
        public static bool operator ==(UnixTimestamp t1, UnixTimestamp td2)
        {
            if (ReferenceEquals(t1, td2))    // Handle both are null or same variable
                return true;
            return !(ReferenceEquals(t1, null) || ReferenceEquals(td2, null)) && t1.SecondsSinceUnixEpoch.Equals(td2.SecondsSinceUnixEpoch);
        }
        
        public static bool operator !=(UnixTimestamp d1, UnixTimestamp d2)
        {
            if (ReferenceEquals(d1, d2))    // Handle both are null or same variable
                return false;
            if (ReferenceEquals(d1, null) || ReferenceEquals(d2, null))
                return true;
            return !d1.SecondsSinceUnixEpoch.Equals(d2.SecondsSinceUnixEpoch);
        }
        
        public static bool operator <(UnixTimestamp t1, UnixTimestamp t2)
        {
            if (ReferenceEquals(t1, t2))
                return false;
            if (ReferenceEquals(t1, null) && ReferenceEquals(t2, null))
                return false;
            if (ReferenceEquals(t1, null))
                return true;
            if (ReferenceEquals(t2, null))
                return false;
            return t1.SecondsSinceUnixEpoch < t2.SecondsSinceUnixEpoch;
        }
        
        public static bool operator <=(UnixTimestamp t1, UnixTimestamp t2)
        {
            if (ReferenceEquals(t1, t2))
                return true;
            if (ReferenceEquals(t1, null) && ReferenceEquals(t2, null))
                return true;
            if (ReferenceEquals(t1, null))
                return true;
            if (ReferenceEquals(t2, null))
                return false;
            return t1.SecondsSinceUnixEpoch <= t2.SecondsSinceUnixEpoch;
        }
        
        public static bool operator >(UnixTimestamp t1, UnixTimestamp t2)
        {
            if (ReferenceEquals(t1, t2))
                return false;
            if (ReferenceEquals(t1, null) && ReferenceEquals(t2, null))
                return false;
            if (ReferenceEquals(t1, null))
                return false;
            if (ReferenceEquals(t2, null))
                return true;
            return t1.SecondsSinceUnixEpoch > t2.SecondsSinceUnixEpoch;
        }
        
        public static bool operator >=(UnixTimestamp t1, UnixTimestamp t2)
        {
            if (ReferenceEquals(t1, t2))
                return true;
            if (ReferenceEquals(t1, null) && ReferenceEquals(t2, null))
                return true;
            if (ReferenceEquals(t1, null))
                return false;
            if (ReferenceEquals(t2, null))
                return true;
            return t1.SecondsSinceUnixEpoch >= t2.SecondsSinceUnixEpoch;
        } 
        #endregion
    }
}
