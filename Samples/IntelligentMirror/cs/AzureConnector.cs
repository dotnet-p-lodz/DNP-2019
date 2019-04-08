using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentMirror
{
    public class AzureConnector
    {
        private readonly string _subscriptionKey;
        private readonly string _uriBase;

        /// <summary>
        ///     Creates AzureConnector for specified base URI
        /// </summary>
        /// <param name="subscriptionKey"></param>
        /// <param name="uriBase"></param>
        public AzureConnector(string subscriptionKey, string uriBase)
        {
            _subscriptionKey = subscriptionKey ?? throw new ArgumentNullException(nameof(subscriptionKey));
            _uriBase = uriBase ?? throw new ArgumentNullException(nameof(uriBase));
            _uriBase += "/detect";
        }

        /// <summary>
        ///     Makes request to Face API endpoint with image provided in a stream and returns obtained JSON response.
        /// </summary>
        /// <param name="imageStream">Analyzed image</param>
        /// <returns>JSON response</returns>
        public async Task<string> MakeAnalysisRequest(Stream imageStream)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

            var requestParameters = "returnFaceId=true" +
                                    "&returnFaceLandmarks=false" +
                                    "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
                                    "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            var uri = _uriBase + "?" + requestParameters;

            using (var content = new StreamContent(imageStream))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await client.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}