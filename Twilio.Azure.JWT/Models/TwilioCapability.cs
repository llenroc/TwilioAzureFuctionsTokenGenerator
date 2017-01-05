﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JWT;

namespace Twilio.JWT
{
    public class TwilioCapability
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly List<ScopeUri> _scopes;

        private string _clientName;

        /// <summary>
        /// Creates a new TwilioCapability token generator for Twilio Client
        /// </summary>
        ///
        /// <param name="accountSid">Your Twilio Account SID from your account dashboard</param>
        /// <param name="authToken">Your Twilio Auth Token from your account dashboard</param>
        public TwilioCapability(string accountSid, string authToken)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _scopes = new List<ScopeUri>();
        }

        /// <summary>
        /// Allow a capability
        /// </summary>
        ///
        /// <param name="service">service to allow</param>
        /// <param name="capability">capability to allow</param>
        /// <param name="prms">params to allow</param>
        public void Allow(string service, string capability, object prms)
        {
            // add to scope
            _scopes.Add(new ScopeUri(service, capability, prms));
        }

        /// <summary>
        /// Allow the Twilio Client device to receive incoming calls with the specified client name
        /// </summary>
        ///
        /// <param name="clientName">The client name to register</param>
        public void AllowClientIncoming(string clientName)
        {
            // clientName must be a non-zero length alphanumeric string
            if (Regex.IsMatch(clientName, @"\W"))
            {
                throw new ArgumentException("Only alphanumeric characters allowed in client name.");
            }

            if (string.IsNullOrEmpty(clientName))
            {
                throw new ArgumentException("Client name must not be a zero length string or NULL.");
            }

            _clientName = clientName;
            Allow("client", "incoming", null);
        }

        /// <summary>
        /// Allow the Twilio Client device to place outgoing calls to the specified Application.
        /// </summary>
        ///
        /// <param name="applicationSid">The Twilio Application SID to connect to.</param>
        public void AllowClientOutgoing(string applicationSid)
        {
            AllowClientOutgoing(applicationSid, null);
        }

        /// <summary>
        /// Allow the Twilio Client device to place outgoing calls to the specified Application.
        /// </summary>
        ///
        /// <param name="applicationSid">The Twilio Application SID to connect to.</param>
        /// <param name="appParams">Optional data to send with the request to your application</param>
        public void AllowClientOutgoing(string applicationSid, object appParams)
        {
            Allow("client", "outgoing", new { appSid = applicationSid, appParams });
        }

        /// <summary>
        /// Generate the token as a string with a custom TTL value (max 24 hours)
        /// </summary>
        ///
        /// <param name="ttlSeconds">The lifespan of the token, in seconds</param>
        public string GenerateToken(int ttlSeconds = 3600)
        {
            var scope = string.Join(" ", _scopes.Select(s => s.ToString(_clientName)).ToArray());
            var payload = new
            {
                iss = _accountSid,
                exp = ConvertToUnixTimestamp(DateTime.UtcNow.AddSeconds(ttlSeconds)),
                scope
            };

            return JsonWebToken.Encode(payload, _authToken, JwtHashAlgorithm.HS256);
        }

        private static int ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var diff = date - origin;
            return (int)Math.Floor(diff.TotalSeconds);
        }
    }
}