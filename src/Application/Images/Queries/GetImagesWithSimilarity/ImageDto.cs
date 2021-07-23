using System;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Images.Commands.GetImagesWithSimilarity
{
    public class ImageDto
    {
        public string Name { get; set; }

        public string Base64 { get; set; }

        public int Similarity { get; set; }
    }
}
