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
                    bitmap.SetSource(stream.CloneStream());
                    Img.Source = bitmap;


                    Img.RenderTransformOrigin = new Point(0.5, 0.5);
                    Img.RenderTransform = new ScaleTransform { ScaleX = -1 };
                    string jsonResponse = await azure.MakeAnalysisRequest(stream);
                    //TODO: (1) Face can be created From Json by providing JSON string as argument. There can be several faces, but now just pick First Or Default one.
                    //Default means that face variable will be null if there are no faces on the photo.
                    Face face = Face.??.??;
                    //TODO: (2) Check, if there are any faces on the photo
                    if (??)
                    {
                        //TODO: (3) Here use functions which will Setup Response and Setup Caption textboxes. Give them face as the argument.
                    }
                    else
                    {
                        //TODO: (4) If there are no faces, provide some default text for the user in both textboxes, ex. "No faces found"
                    }
                }
            }
        }

        private void SetupResponse(Face face)
        {
            //TODO: (5) Extract Age, Gender and Glasses information from face.FaceAttributes and add them to the response text.
            string response = $"Age: {16 /*replace this!*/}\n";
            response += $"Glasses: {false /*replace this!*/}\n";
            ResponseBox.Text = response;
        }

        private void SetupCaption(Face face)
        {
            string caption = $"{face.FaceAttributes.Age}-year-old ";

            //TODO: (6) Extract Gender and Glasses information, but present them in nice, human-readable way
            //Ex. "22-year-old man wearing glasses" or "19-year-old woman".
            //Try taking gender and age into account to write boy/man and girl/woman.
            //Add everything to the caption variable

            CaptionBox.Text = caption;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
