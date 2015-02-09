/****************************** Module Header ******************************\
Module Name:  GeniLocation.cs
Project:      GeniApiSample
Copyright (c) Bjørn P. Brox <bjorn.brox@gmail.com>

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System.Text;

namespace GeniApiSample
{
    /// <summary>
    /// Returns location elements
    /// See http://www.geni.com/platform/developer/help/api?path=location
    /// </summary>
    public class GeniLocation
    {
        public JsonDictionary Json { get; private set; }
        
        /// <summary>
        /// Place name
        /// </summary>
        public string PlaceName { get; private set; }

        /// <summary>
        /// City name
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// County name
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// State name
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public double? Latitude { get; private set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public double? Longitude { get; private set; }

        public GeniLocation(JsonDictionary jsonDict)
        {
            Json = jsonDict;

            PlaceName = Json["place_name"] as string;
            City = Json["city"] as string;
            County = Json["county"] as string;
            State = Json["state"] as string;
            Country = Json["country"] as string;

            Latitude = Json["latitude"] as double?;
            Longitude = Json["longitude"] as double?;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(PlaceName);
            if (City != null)
                sb.AppendFormat("{0}{1}", sb.Length > 0 ? ", " : "", City);
            if (County != null)
                sb.AppendFormat("{0}{1}", sb.Length > 0 ? ", " : "", County);
            if (State != null)
                sb.AppendFormat("{0}{1}", sb.Length > 0 ? ", " : "", State);
            if (Country != null)
                sb.AppendFormat("{0}{1}", sb.Length > 0 ? ", " : "", Country);
            return sb.ToString();
        }
    }
}
