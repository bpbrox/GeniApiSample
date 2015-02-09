/****************************** Module Header ******************************\
Module Name:  GeniProfile.cs
Project:      GeniApiSample
Copyright (c) Bjørn P. Brox <bjorn.brox@gmail.com>

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

namespace GeniApiSample
{
    /// <summary>
    /// Geni profile
    /// For all items see http://www.geni.com/platform/developer/help/api?path=profile
    /// </summary>
    public class GeniProfile
    {
        public JsonDictionary Json { get; private set; }

        /// <summary>
        /// The profile's node id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The profile's name as it appears on the site to the current user
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// True if the profile is claimed by a user
        /// </summary>
        public bool? Claimed { get; private set; }

        /// <summary>
        /// True if the profile is attached to the big tree
        /// </summary>
        public bool? BigTree { get; private set; }

        /// <summary>
        /// The users acount type
        /// </summary>
        public string AccountType { get; private set; }

        /// <summary>
        /// The Profile's email address
        /// </summary>
        public string EMail { get; private set; }

        /// <summary>
        /// Timestamp of when the profile was created
        /// </summary>
        public UnixTimestamp CreatedAt { get; private set; }

        public GeniLocation CurrentResidence { get; private set; }

        public GeniProfile(JsonDictionary jsonDict)
        {
            Json = jsonDict;
            Id = Json["id"] as string;
            Name = Json["name"] as string;
            Claimed = Json["claimed"] as bool?;
            BigTree = Json["big_tree"] as bool?;
            AccountType = Json["account_type"] as string;
            EMail = Json["email"] as string;
            long tmpLong;
            if (long.TryParse(Json["created_at"] as string, out tmpLong))
                CreatedAt = new UnixTimestamp(tmpLong);
            JsonDictionary jd = Json["current_residence"] as JsonDictionary;
            if (jd != null)
                CurrentResidence = new GeniLocation(jd);
        }

        public object this[string key]
        {
            get
            {
                return Json[key];
            }
        }

    }
}
