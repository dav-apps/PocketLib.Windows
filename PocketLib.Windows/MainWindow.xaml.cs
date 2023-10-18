using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using PocketLib.Windows.Models;
using System;
using System.Diagnostics;
using Windows.UI;

namespace PocketLib.Windows
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(TitleBar);
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            WebView.Close();
        }

        private void WebView_WebMessageReceived(WebView2 sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                string message = args.TryGetWebMessageAsString();
                var messageData = JsonConvert.DeserializeObject<WebMessage>(message);
                Debug.WriteLine(messageData.Theme);

                Color color = Color.FromArgb(255, 0, 0, 0);
                
                if (messageData.Theme == "dark")
                    color = Color.FromArgb(255, 255, 255, 255);

                (Application.Current.Resources["WindowCaptionForeground"] as SolidColorBrush).Color = color;
                (Application.Current.Resources["WindowCaptionForegroundDisabled"] as SolidColorBrush).Color = color;
            }
            catch (Exception) { }
        }
    }
}
