using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using HttpRequestComposer.HttpManager;
using HttpRequestComposer.Models;
using Newtonsoft.Json;

namespace HttpRequestComposer
{
    public partial class MainWindow : Window
    {
        public HttpRequestModel Model { get; set; } = new HttpRequestModel();

        public MainWindow()
        {
            DataContext = this;
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
            Model.IsBusy = true;
            await new HttpRequestManager(Model).SendRequestAsync().ContinueWith(response =>
            {
                SetResponseText(response.Exception?.ToString() ?? response.Result.ToString());
            }).ContinueWith(response =>
            {
                Model.IsBusy = false;
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
