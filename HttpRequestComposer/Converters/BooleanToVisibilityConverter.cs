using System.Windows;

namespace HttpRequestComposer
{
    public sealed class BooleanToVisibilityConverter : VisibilityConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}