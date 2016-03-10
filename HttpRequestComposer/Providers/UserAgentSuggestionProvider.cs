namespace HttpRequestComposer
{
    public class UserAgentSuggestionProvider : SuggestionFromResource
    {
        public UserAgentSuggestionProvider() : base("HttpRequestComposer.user-agents.json")
        {
        }
    }
}