public class AIService
{
    private readonly HttpClient _client;

    public AIService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("OpenAI");
    }

    public async Task<string> AskAIAsync(string prompt)
    {
        var response = await _client.PostAsJsonAsync("/openai/deployments/gpt-4o-mini/chat/completions", new {
            messages = new[] {
                new { role = "system", content = "You are a personal branding assistant." },
                new { role = "user", content = prompt }
            }
        });
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        return json.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
    }
}
