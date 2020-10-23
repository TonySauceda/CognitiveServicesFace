using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CognitiveServicesFace.Models;

namespace CognitiveServicesFace.Data
{
    public class CognitiveServicesFaceContext : DbContext
    {
        public CognitiveServicesFaceContext (DbContextOptions<CognitiveServicesFaceContext> options)
            : base(options)
        {
        }

        public DbSet<PictureModel> Pictures { get; set; }
        public DbSet<FaceModel> Faces { get; set; }
        public DbSet<EmotionModel> Emotions { get; set; }
    }
}
