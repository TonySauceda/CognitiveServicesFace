using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Rest;
using System.IO;
using CognitiveServicesFace.Models;
using System.Collections.ObjectModel;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Reflection;

namespace CognitiveServicesFace.Helpers
{
    public class FaceHelper
    {
        public FaceClient faceClient;

        public FaceHelper(string subscriptionKey)
        {
            faceClient = new FaceClient(new ApiKeyServiceClientCredentials(subscriptionKey));
            faceClient.Endpoint = "https://tonysauceda-cognitive-service-face.cognitiveservices.azure.com/";
        }

        public async Task<PictureModel> DetectAndExtractFaceAsync(Stream imageStream)
        {
            var faceAttributes = new FaceAttributeType?[] { FaceAttributeType.Emotion };
            var faces = await faceClient.Face.DetectWithStreamAsync(imageStream, true, false, faceAttributes);

            var picture = new PictureModel();
            ExtractFaces(faces, picture);

            return picture;
        }

        private void ExtractFaces(IList<DetectedFace> faces, PictureModel picture)
        {
            picture.Faces = new ObservableCollection<FaceModel>();

            foreach (var face in faces)
            {
                var faceModel = new FaceModel()
                {
                    Left = face.FaceRectangle.Left,
                    Height = face.FaceRectangle.Height,
                    Top = face.FaceRectangle.Top,
                    Width = face.FaceRectangle.Width,
                    Picture = picture
                };

                ExtractEmotions(face.FaceAttributes.Emotion, faceModel);

                picture.Faces.Add(faceModel);
            }
        }

        private void ExtractEmotions(Emotion emotion, FaceModel face)
        {
            face.Emotions = new ObservableCollection<EmotionModel>();

            var properties = emotion.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            properties = properties.Where(x => x.PropertyType == typeof(double)).ToArray();

            foreach (var prop in properties)
            {
                var typeEmotion = EmotionEnum.Undetermined;
                Enum.TryParse<EmotionEnum>(prop.Name, out typeEmotion);

                face.Emotions.Add(new EmotionModel()
                {
                    Score = (double)prop.GetValue(emotion),
                    Type = typeEmotion,
                    Face = face
                });
            }
        }
    }
}
