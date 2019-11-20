namespace Fashionista.Application.Brands.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    using static Fashionista.Common.GlobalConstants;

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, int>
    {
        private readonly IDeletableEntityRepository<Brand> brandsRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public CreateBrandCommandHandler(
            IDeletableEntityRepository<Brand> brandsRepository,
            ICloudinaryHelper cloudinaryHelper)
        {
            this.brandsRepository = brandsRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (await CommonCheckAssistant.CheckIfBrandWithSameNameExists(request.Name, this.brandsRepository))
            {
                throw new EntityAlreadyExistsException(nameof(Brand), request.Name);
            }

            var brand = new Brand
            {
                Name = request.Name,
                BrandPhotoUrl = await this.UploadImage(request),
            };

            await this.brandsRepository.AddAsync(brand);
            await this.brandsRepository.SaveChangesAsync(cancellationToken);

            return brand.Id;
        }

        private async Task<string> UploadImage(CreateBrandCommand request)
        {
            return await this.cloudinaryHelper.UploadImage(
                request.Photo,
                name: $"{request.Name}-brand-main-shot",
                transformation: new Transformation().Width(BRAND_IMAGE_WIDTH).Height(BRAND_IMAGE_HEIGHT));
        }
    }
}
