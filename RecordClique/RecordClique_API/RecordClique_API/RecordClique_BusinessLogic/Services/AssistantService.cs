using Microsoft.AspNetCore.Http.HttpResults;
using OpenAI.Chat;
using RecordClique_BusinessLogic.Services.Abstractions;

namespace RecordClique_BusinessLogic.Services
{
    public class AssistantService : IAssistantService
    {
        public object GetAssistantResponse(string text)
        {
            string apiKey = "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";
            ChatClient client = new(model: "gpt-4.1-nano", apiKey: apiKey);
            var completion = client.CompleteChat("I want just music suggestions. I want a single answer, not a conversation. Make a numbered list when it comes to a list because it's not formatted text. " +
                "If what will I write here won't be music related, tell me something like \" It's not music related. Let's talk only about music for the moment. :) \"" +
                " The message is: " + text);
            return new { Message = completion.Value.Content[0].Text };
        }
    }
}
