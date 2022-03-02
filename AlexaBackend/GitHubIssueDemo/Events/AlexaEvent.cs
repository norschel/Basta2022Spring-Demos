namespace GitHubIssueDemo.Events
{
    public class AlexaEvent
    {
        public AlexaEvent()
        {
            client_payload = new AlexaEventPayload();
        }

        public string event_type { get; set; }
        public AlexaEventPayload client_payload { get; set; }
    }


}

