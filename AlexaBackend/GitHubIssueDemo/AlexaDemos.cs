using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET;
using Alexa.NET.Response;

namespace GitHubIssueDemo
{

    public static class AlexaDemos
    {
        [FunctionName("Alexa")]
        public static async Task<IActionResult> Run(
 [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
 ILogger log)
        {
            string json = await req.ReadAsStringAsync();
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            var requestType = skillRequest.GetRequestType();
            SkillResponse response = null;

            if (requestType == typeof(LaunchRequest))
            {
                response = ResponseBuilder.Tell("Herzlich willkommen zur Basta Session von Neno und Nico!");
                response.Response.ShouldEndSession = false;
            }

            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = skillRequest.Request as IntentRequest;

                if (intentRequest.Intent.Name == "erstellegithubissue")
                {
                    response = ResponseBuilder.Tell($"Ich habe ein Ticket für {intentRequest.Intent.Slots["name"].Value} erstellt. Das ist so großartig.");
                    response.Response.ShouldEndSession = true;

                    GitHubClient ghClient = new GitHubClient();
                    ghClient.CallRepoWebHook(intentRequest.Intent.Slots["name"].Value);

                }
            }

            return new OkObjectResult(response);
        }

    }

}

