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

                    Face face = Face.FromJson(jsonResponse).FirstOrDefault();
                    if (face != null)
                    {
                        SetupResponse(face);
                        SetupCaption(face);
                    }
                    else
                    {
                        ResponseBox.Text = "No faces found";
                        CaptionBox.Text = "No faces found";
                    }
                }
            }
        }

        private void SetupResponse(Face face)
        {
            string response = $"Age: {face.FaceAttributes.Age}\n";
            response += $"Gender: {face.FaceAttributes.Gender}\n";
            response += $"Glasses: {face.FaceAttributes.Glasses}\n";

            string emotionText = GetHighestEmotion(face.FaceAttributes.Emotion); //TODO: (1) Implement the function
            //TODO: (2) Remember to add emotionText to the response!
            //Your code here. One line only!

            string hairText = GetHighestHair(face.FaceAttributes.Hair.HairColor); //TODO: (3) Implement the function
            //TODO: (4) Remember to add hairText to the response!
            //Your code here. One line only!

            ResponseBox.Text = response;
        }

        private void SetupCaption(Face face)
        {
            string caption = $"{face.FaceAttributes.Age}-year-old ";

            if (face.FaceAttributes.Gender == "male")
            {
                caption += "man ";
            }
            else
            {
                caption += "woman ";
            }

            if (face.FaceAttributes.Glasses != "NoGlasses")
            {
                caption += "wearing glasses";
            }

            //TODO: (5) Using the same functions as in SetupResponse, make caption text more detailed
            //Your code here

            CaptionBox.Text = caption;
        }

        private string GetHighestEmotion(Emotion emotion)
        {
            string emotionText = "Neutral";
            double highestConfidence = 0.0;
            //TODO: (1.1) Check if some emotion (ex. emotion.Anger) is higher than highestConfidence til now
            if (??)
            {
                //TODO: (1.2) If yes, then set new emotionText and overwrite highestConfidence with new value
                //Your code here
            }

            //TODO: (1.3) Do it for several emotions, ex. happiness, surprise, disgust etc.
            //Your code here

            return emotionText;
        }

        private static string GetHighestHair(List<HairColor> hairColors)
        {
            string hairText = "other";
            double highestConfidence = 0.0;
            foreach (HairColor hairColor in hairColors)
            {
                //TODO: (3.1) Check if current hairColor.Confidence is higher than highestConfidence
                if (??)
                {
                    //TODO: (3.2) If yes, then set new hairText to hairColor.Color and overwrite highestConfidence with new value
                    //Your code here
                }
            }

            return hairText;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
