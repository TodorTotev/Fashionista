namespace Fashionista.Application.Products.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Brand> brandsRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public CreateProductCommandHandler(
            IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Brand> brandsRepository,
            ICloudinaryHelper cloudinaryHelper)
        {
            this.productsRepository = productsRepository;
            this.brandsRepository = brandsRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfBrandExists(request.BrandId, this.brandsRepository))
            {
                throw new NotFoundException(nameof(Brand), request.BrandId);
            }

            if (await CommonCheckAssistant.CheckIfProductWithSameNameExists(request.Name, this.productsRepository))
            {
                throw new EntityAlreadyExistsException(nameof(Product), request.Name);
            }

            var product = request.To<Product>();

            if (request.Photos != null)
            {
                foreach (var photo in request.Photos)
                {
                    product.Photos.Add(await this.UploadImage(photo));
                    product.Photos.Remove("Microsoft.AspNetCore.Http.FormFile");
                }
            }

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        private async Task<string> UploadImage(IFormFile photo)
        {
            return await this.cloudinaryHelper.UploadImage(
                photo,
                name: $"{photo.Name}-product-item-shot");
        }
    }
}
