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

                    CaptionBox.Text = "";
                    ResponseBox.Text = "";
                    List<Face> faces = Face.FromJson(jsonResponse);
                    foreach (Face face in faces)
                    {
                        SetupResponse(face);
                        SetupCaption(face);
                    }

                    if (faces.Count < 1)
                    {
                        ResponseBox.Text = "No faces found";
                        CaptionBox.Text = "No faces found";
                    }
                }
            }
        }

        private void SetupResponse(Face face)
        {
            string response = $"\nAge: {face.FaceAttributes.Age}\n";
            response += $"Gender: {face.FaceAttributes.Gender}\n";
            response += $"Glasses: {face.FaceAttributes.Glasses}\n";

            string emotionText = GetHighestEmotion(face.FaceAttributes.Emotion);
            response += $"Emotion: {emotionText}\n";

            string hairText = GetHighestHair(face.FaceAttributes.Hair.HairColor);
            response += $"Hair: {hairText}\n";

            ResponseBox.Text += response;
        }

        private void SetupCaption(Face face)
        {
            string caption = $"\n{face.FaceAttributes.Age}-year-old ";

            if (face.FaceAttributes.Gender == "male")
            {
                caption += "man ";
            }
            else
            {
                caption += "woman ";
            }

            caption += $"with {GetHighestHair(face.FaceAttributes.Hair.HairColor)} hair ";

            if (face.FaceAttributes.Glasses != "NoGlasses")
            {
                caption += "wearing glasses";
            }

            CaptionBox.Text += caption;
        }

        private string GetHighestEmotion(Emotion emotion)
        {
            string emotionText = "Neutral";
            double highestConfidence = 0.0;
            if (emotion.Anger > highestConfidence)
            {
                emotionText = "Anger";
                highestConfidence = emotion.Anger;
            }
            if (emotion.Happiness > highestConfidence)
            {
                emotionText = "Happiness";
                highestConfidence = emotion.Happiness;
            }
            if (emotion.Disgust > highestConfidence)
            {
                emotionText = "Disgust";
                highestConfidence = emotion.Disgust;
            }
            if (emotion.Surprise > highestConfidence)
            {
                emotionText = "Surprise";
                highestConfidence = emotion.Surprise;
            }

            return emotionText;
        }

        private static string GetHighestHair(List<HairColor> hairColors)
        {
            string hairText = "other";
            double highestConfidence = 0.0;
            foreach (HairColor hairColor in hairColors)
            {
                if (hairColor.Confidence > highestConfidence)
                {
                    hairText = hairColor.Color;
                    highestConfidence = hairColor.Confidence;
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
