using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServicesFace.Models
{
    public class PictureModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public virtual ObservableCollection<FaceModel> Faces { get; set; }
    }
}
