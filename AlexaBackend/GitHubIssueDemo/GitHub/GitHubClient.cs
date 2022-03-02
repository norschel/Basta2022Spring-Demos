using GitHubIssueDemo.Events;
using RestSharp;
using System;

namespace GitHubIssueDemo
{
    public class GitHubClient
    {
        readonly RestClient _client;

        public GitHubClient()
        {
            var githubPAT = Environment.GetEnvironmentVariable("GitHubPAT");
            if (string.IsNullOrEmpty(githubPAT))
            {
                Environment.Exit(100);
            }
            else
            {
                Console.WriteLine("Found github pat");
            }

            _client = new RestClient("https://api.github.com/")
                .AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json")
                .AddDefaultHeader(KnownHeaders.Authorization, "token " + githubPAT);
        }

        public void CallRepoWebHook(string name)
        {
            var param = new AlexaEvent
            {
                event_type = "publish_alexa",
            };
            param.client_payload.name = name;


            RestRequest restRequest = new RestRequest("repos/norschelMVP/AlexaDemo/dispatches", Method.Post);
            restRequest.AddJsonBody(param);

            _client.ExecuteAsync(restRequest);
        }

    }


}

