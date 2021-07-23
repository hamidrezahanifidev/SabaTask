using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Helpers;
using System.Collections.Generic;
using System.Drawing.Imaging;
using CleanArchitecture.Application.Common.Exceptions;

namespace CleanArchitecture.Application.Images.Commands.GetImagesWithSimilarity
{
    public class GetImagesWithSimilarityQuery : IRequest<ImagesVm>
    {
        public string Image { get; set; }
    }

    public class GetImagesWithSimilarityQueryHandler : IRequestHandler<GetImagesWithSimilarityQuery, ImagesVm>
    {
        private readonly IApplicationDbContext _context;

        public GetImagesWithSimilarityQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ImagesVm> Handle(GetImagesWithSimilarityQuery request, CancellationToken cancellationToken)
        {
            Image bodyToImage = ImageHelper.BodyToImage(request.Image);

            if (!bodyToImage.RawFormat.Equals(ImageFormat.Jpeg))
            {
                throw new ImageTypeException();
            }

            Random rnd = new Random();
            List<ImageDto> list = new List<ImageDto>();
            int count = rnd.Next(1, 6);

            var items = await _context.Images
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .Take(count)
                .ToListAsync(cancellationToken);

            if ( items == null)
            {
                throw new NotFoundException();
            }

            foreach ( var item in items)
            {
                Image image = ImageHelper.GetImageByPath(item.Address);

                list.Add(new ImageDto(){
                    Base64 = ImageHelper.ImageToBase64(image),
                    Name = item.Name,
                    Similarity = rnd.Next(1, 100)
                });
            }

            return new ImagesVm { Images = list };
        }
    }
}
