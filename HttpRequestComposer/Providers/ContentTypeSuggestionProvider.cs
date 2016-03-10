namespace HttpRequestComposer
{
    public class ContentTypeSuggestionProvider : SuggestionFromResource
    {
        public ContentTypeSuggestionProvider() : base("HttpRequestComposer.media-types.json")
        {
        }
    }
}