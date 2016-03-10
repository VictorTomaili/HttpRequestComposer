using System;
using System.Windows;
using System.Windows.Input;
using HttpRequestComposer.HttpManager;
using HttpRequestComposer.Models;

namespace HttpRequestComposer
{
    public partial class MainWindow : Window
    {
        public MainWindowModel Model { get; set; } = new MainWindowModel();

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private void RunButton_OnClick(object sender, RoutedEventArgs e)
        {
            SendHttpRequest();
        }

        private void TextBox_OnEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            RunButton.Focus();
            SendHttpRequest();
        }

        private async void SendHttpRequest()
        {
            await new HttpRequestManager(Model).SendRequestAsync().ContinueWith(response =>
            {
                SetResponseText(response.Exception?.ToString() ?? response.Result.ToString());
            });
        }

        private void SetResponseText(string value)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action<string>(SetResponseText), value);
                return;
            }

            Model.Response = value;
        }
    }
}
