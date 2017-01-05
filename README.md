# Create Twilio JWT Tokens with Azure Functions

Twilio’s SDKs require access tokens to authorize users. Generating these tokens must be done on a server. So why not use [Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview) to generate these tokens for you? Azure Functions has a competitive [pricing model](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview#pricing), you run it locally in many programming languages, and even debug it locally. Now you can focus on making your next great mobile app with [Programmable Video](https://www.twilio.com/video), [Programmable Chat](https://www.twilio.com/chat) or [Programmable Voice](https://www.twilio.com/voice).

#### Azure Functions Background
>Azure Functions is a solution for easily running small pieces of code, or "functions," in the cloud. You can write just the code you need for the problem at hand, without worrying about a whole application or the infrastructure to run it. Functions can make development even more productive, and you can use your development language of choice, such as C#, F#, Node.js, Python or PHP. Pay only for the time your code runs and trust Azure to scale as needed.

## How to Use This Project
 - Full documentation with pictures coming soon.
 
### 1. Gather Account Information

The first thing we need to do is grab all the necessary configuration values from our
Twilio account. To set up our back-end for Chat, we will need four 
pieces of information:

| Config Value  | Description |
| :-------------  |:------------- |
Account SID | Your primary Twilio account identifier - find this [in the console here](https://www.twilio.com/console/chat/getting-started).
API Key | Used to authenticate - [generate one here](https://www.twilio.com/console/chat/dev-tools/api-keys).
API Secret | Used to authenticate - [just like the above, you'll get one here](https://www.twilio.com/console/chat/dev-tools/api-keys).
Service Instance SID | Like a database for your Chat data - [generate one in the console here](https://www.twilio.com/console/chat/services)

### 2. Put these values in appsettings.json
```
{
  ...
  "Values": {
    "TwilioAccountSid": "",
    "TwilioApiKey": "",
    "TwilioApiSecret": "",
    "TwilioIPMServiceSid": ""
  }
}
```

### 3. Run the project locally or push to Azure!
Need help pushing this to Azure? Take a look at this [step by step guide](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function-azure-portal).

## Tips
 - Fun Fact, Azure Functions doesn't like "."s in project names. This is why the Azure function is called Token.
 
## Requirements
- [Microsoft Azure Account](https://azure.microsoft.com/en-us/services/functions/)
- Windows
 - [Visual Studio for Windows](https://www.visualstudio.com/downloads/)
 - [Azure .NET SDK](https://go.microsoft.com/fwlink/?LinkId=518003&clcid=0x409)
 - [Visual Studio Tools for Azure Functions](https://aka.ms/azfunctiontools)
- Mac
 - [Visual Studio Code](https://code.visualstudio.com/)
 - [Azure Functions CLI](https://www.npmjs.com/package/azure-functions-cli)
 
## Resources
 - [Get Started With Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function)
 - [Running Azure Functions Locally](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
 - [Running Azure Functions blog post](https://blogs.msdn.microsoft.com/appserviceteam/2016/12/01/running-azure-functions-locally-with-the-cli/)
 - [How To Deploy Azure Functions From GitHub](http://jameschambers.com/2016/11/deploy-functions-from-github/)
