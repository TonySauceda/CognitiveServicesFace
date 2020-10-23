using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServicesFace.Models
{
    public class FaceModel
    {
        public int Id { get; set; }
        public int PictureId { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public virtual PictureModel Picture { get; set; }
        public virtual ObservableCollection<EmotionModel> Emotions { get; set; }
    }
}
