#r "..\..\libs\Twilio.Azure.JWT.dll"
using System;
using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // Parse query parameter here.
    // Specifically looking for "device" for this ip chat example.
    string device = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key.ToLower(), "device", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    device = device ?? data?.device;

    // See appsettings.json or use azure environment variables
    var ipmGrant = new Twilio.JWT.IpMessagingGrant
    {
        ServiceSid = Environment.GetEnvironmentVariable("TwilioIPMServiceSid"),
        EndpointId = $"JWTAzureFunctions.{ DateTime.Now.ToUniversalTime() }.{ device }", // TODO: Change this string to a unique endpoint
        DeploymentRoleSid = string.Empty, // not used in this example
        PushCredentialSid = string.Empty // note used in this example
    };

    // See appsettings.json or use azure environment variables
    var accessToken = new Twilio.JWT.AccessToken(
        Environment.GetEnvironmentVariable("TwilioAccountSid"),
        Environment.GetEnvironmentVariable("TwilioApiKey"),
        Environment.GetEnvironmentVariable("TwilioApiSecret"));

    // Add ip chat grant 
    accessToken.AddGrant(ipmGrant);
    
    // Anonymous object to be returned by response
    var json = new { token = accessToken.ToString(), identity = device };

    // Return response
    return device == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a device name in the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, json); // return json
}