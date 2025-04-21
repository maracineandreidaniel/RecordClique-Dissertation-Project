using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_BusinessLogic.Services
{
    public class AssistantService : IAssistantService
    {

        private readonly string _apiKey;

        public AssistantService(IConfiguration config)
        {
            _apiKey = config["APIKeys:OpenAI"] ?? "";
        }


        public object GetAssistantResponse(string text)
        {
            string fallback = "The answer for your question is: 1. AC/DC - Back in Black, 2. Queen - A night at the opera, 3. Pink Floyd - Illusions, 4. Van Halen - Van Halen 1, 5. Metallica - Master of Puppets";

            try
            {
                ChatClient client = new(model: "gpt-4.1-nano", apiKey: _apiKey);
                var completion = client.CompleteChat("I want just music suggestions. I want a single answer, not a conversation. Make a numbered list when it comes to a list because it's not formatted text. " +
                    "If what will I write here won't be music related, tell me something like \" It's not music related. Let's talk only about music suggestions for the moment. :) \"" +
                    " The message is: " + text);

                return new { Message = completion?.Value?.Content?[0]?.Text ?? fallback };
            }
            catch
            {
                return new { Message = fallback };
            }
        }

    }
}
