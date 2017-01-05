﻿using System.Collections.Generic;

namespace Twilio.JWT
{
    /// <summary>
    /// Grant to expose Twilio Video
    /// </summary>
    public class VideoGrant : IGrant
    {
        public string ConfigurationProfileSid { get; set; }

        /// <summary>
        /// Get the Video grant key
        /// </summary>
        ///
        /// <returns>the video grant key</returns>
        public string GetGrantKey()
        {
            return "video";
        }

        /// <summary>
        /// Get the video grant payload
        /// </summary>
        ///
        /// <returns>the video grant payload</returns>
        public object GetPayload()
        {
            var payload = new Dictionary<string, object>();
            if (ConfigurationProfileSid != null)
            {
                payload.Add("configuration_profile_sid", ConfigurationProfileSid);
            }

            return payload;
        }
    }
}