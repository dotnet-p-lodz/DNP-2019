using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IntelligentMirror.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResponsePage : Page
    {
        private AzureConnector azure = new AzureConnector("<SUBSCRIPTION KEY>", @"<BASE URI>/vision/v1.0");
        public ResponsePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //TODO: (2) What type has our file on Main Page?
            ?? passedFile = e.Parameter as ??;

            //TODO: (3) Make sure passedFile is not null - check if it's different from null (replace '??' with condition check)
            if (??)
            {
                using (IRandomAccessStream stream = await passedFile.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmap = new BitmapImage();
                    //TODO: (4) Now our file is stored in stream variable. Set Source of the bitmap to that stream
                    //Your code here. Only one line! 
                    //TODO: (5) And now set Source of the Image from the XAML view to that bitmap (just assign; Source is not a method)
                    //Your code here. Only one line!

                    //TODO: This code flips the image horizontally. Just replace ?? with x:Name of your Image from XAML view
                    ??.RenderTransformOrigin = new Point(0.5, 0.5);
                    ??.RenderTransform = new ScaleTransform { ScaleX = -1 };
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
