using System;
using System.Collections.Generic;

namespace art_gallery_api.Models
{
    public class Artifact
    {
        public Artifact (){}
        public int ArtifactId { get; set; }
        public string ArtifactTitle { get; set; } = null!;
        public string StyleId { get; set; } = null!;
        public string ArtistId { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsInGallery { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
