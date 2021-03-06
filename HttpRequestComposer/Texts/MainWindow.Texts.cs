﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace HttpRequestComposer.Local.MainWindow
{
    public static class Main
    {
        public static string Title => "Http Request Composer";
        public static string CustomMethod => "Custom Method";
        public static string Run => "Run";
        public static string Select => "Select";
        public static string Edit => "Edit";

        public static string ContentType => "Content Type:";
        public static string UserAgent => "User Agent:";
        public static string Headers => "Headers:";
        public static string HeaderPerLine => "HeaderPerLine:";
        public static string Body => "Body:";

        public static string PleaseWait => "Please Wait";
    }
}
