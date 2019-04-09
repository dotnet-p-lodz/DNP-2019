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
using IntelligentMirror.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IntelligentMirror.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResponsePage : Page
    {
        //TODO: Provide subscription key and uri of Microsoft Azure Face API service (given by a person conducting the workshop)
        private AzureConnector azure = new AzureConnector("<SUBSCRIPTION KEY>", @"<BASE URI>/vision/v1.0");
        public ResponsePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            StorageFile passedFile = e.Parameter as StorageFile;

            if (passedFile != null)
            {
                using (IRandomAccessStream stream = await passedFile.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmap = new BitmapImage();
                    //TODO: (1) Streams can be used only once, so you have to Clone Stream here, to be able to use it later again
                    bitmap.SetSource(stream??);
                    Img.Source = bitmap;


                    Img.RenderTransformOrigin = new Point(0.5, 0.5);
                    Img.RenderTransform = new ScaleTransform { ScaleX = -1 };
                    //TODO: (2) Make analysis request to Microsoft Azure using data of the image. Where is it stored?
                    string jsonResponse = await azure.MakeAnalysisRequest(??);
                    //TODO: (3) Display somewhere received raw data, ex. in one of the TextBoxes
                    //Your code here
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
