using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServicesFace.Models
{
    public class EmotionModel
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int FaceId { get; set; }
        public EmotionEnum Type { get; set; }

        public virtual FaceModel Face { get; set; }
        public virtual string TypeText { get => Type.ToString(); }
    }
}
