using System;
using System.Collections.Generic;

namespace art_gallery_api.Models
{
    public class Exhibition
    {
        public Exhibition (){}
        public int ExhibitionId { get; set; }
        public string ExhibitionTitle { get; set; } = null!;
        public string FeaturingArtStyle { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime ExhibitionDate { get; set; }
        public int? ExpectedCrowd { get; set; }
    }
}
