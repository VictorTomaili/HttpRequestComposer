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
            InitializeComponent();
            this.DataContext = Model;
        }

        private void RunButton_OnClick(object sender, RoutedEventArgs e)
        {
            SendHttpRequest();
        }

        private void HostAddressTextBox_OnEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            RunButton.Focus();
            SendHttpRequest();
        }

        private async void SendHttpRequest()
        {
            if (string.IsNullOrEmpty(Model.Url))
            {
                MessageBox.Show("Uri is invalid");
                return;
            }

            if (!Model.Url.IsUrl())
                Model.Url = $"http://{Model.Url}";

            var responseManager = new HttpRequestManager(Model.Url);

            try
            {
                responseManager.AddHeaderRange(Model.Headers);
                responseManager.SetContent(Model.ContentType);
                responseManager.AddHeader("User-Agent", Model.UserAgent);

                await responseManager.SendAsync(Model.HttpMethod).ContinueWith(response =>
                {
                    if (response.Exception != null)
                    {
                        SetResponseText(response.Exception.ToString());
                        return;
                    }

                    SetResponseText(response.Result.AsFormattedString());
                });
            }
            catch (Exception ex)
            {
                SetResponseText(ex.InnerExceptionMessage());
            }
            finally
            {
                responseManager.Dispose();
            }
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
