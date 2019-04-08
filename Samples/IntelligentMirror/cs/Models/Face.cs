using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntelligentMirror.Models
{
    public class Face
    {
        public Guid FaceId { get; set; }
        public FaceRectangle FaceRectangle { get; set; }
        public FaceAttributes FaceAttributes { get; set; }

        public static List<Face> FromJson(string json) => JsonConvert.DeserializeObject<List<Face>>(json);
    }

    public class FaceAttributes
    {
        public long Smile { get; set; }
        public HeadPose HeadPose { get; set; }
        public string Gender { get; set; }
        public double Age { get; set; }
        public FacialHair FacialHair { get; set; }
        public string Glasses { get; set; }
        public Emotion Emotion { get; set; }
        public Blur Blur { get; set; }
        public Exposure Exposure { get; set; }
        public Noise Noise { get; set; }
        public Makeup Makeup { get; set; }
        public List<object> Accessories { get; set; }
        public Occlusion Occlusion { get; set; }
        public Hair Hair { get; set; }
    }

    public class Blur
    {
        public string BlurLevel { get; set; }
        public double Value { get; set; }
    }

    public class Emotion
    {
        public long Anger { get; set; }
        public long Contempt { get; set; }
        public long Disgust { get; set; }
        public long Fear { get; set; }
        public long Happiness { get; set; }
        public double Neutral { get; set; }
        public double Sadness { get; set; }
        public double Surprise { get; set; }
    }

    public class Exposure
    {
        public string ExposureLevel { get; set; }
        public double Value { get; set; }
    }

    public class FacialHair
    {
        public long Moustache { get; set; }
        public long Beard { get; set; }
        public long Sideburns { get; set; }
    }

    public class Hair
    {
        public long Bald { get; set; }
        public bool Invisible { get; set; }
        public List<HairColor> HairColor { get; set; }
    }

    public class HairColor
    {
        public string Color { get; set; }
        public double Confidence { get; set; }
    }

    public class HeadPose
    {
        public long Pitch { get; set; }
        public double Roll { get; set; }
        public double Yaw { get; set; }
    }

    public class Makeup
    {
        public bool EyeMakeup { get; set; }
        public bool LipMakeup { get; set; }
    }

    public class Noise
    {
        public string NoiseLevel { get; set; }
        public long Value { get; set; }
    }

    public class Occlusion
    {
        public bool ForeheadOccluded { get; set; }
        public bool EyeOccluded { get; set; }
        public bool MouthOccluded { get; set; }
    }

    public class FaceRectangle
    {
        public long Top { get; set; }
        public long Left { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
    }
}