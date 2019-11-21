namespace Fashionista.Application.Products.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, int>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Brand> brandsRepository;
        private readonly ICloudinaryHelper cloudinaryHelper;

        public EditProductCommandHandler(
            IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Brand> brandsRepository,
            ICloudinaryHelper cloudinaryHelper)
        {
            this.productsRepository = productsRepository;
            this.brandsRepository = brandsRepository;
            this.cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<int> Handle(EditProductCommand request, CancellationToken cancellationToken)
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

            var requestedEntity = await this.productsRepository
                                      .GetByIdWithDeletedAsync(request.Id)
                                  ?? throw new NotFoundException(nameof(Product), request.Id);

            requestedEntity.Name = request.Name;
            requestedEntity.Description = request.Description;
            requestedEntity.Price = request.Price;
            requestedEntity.IsHidden = request.IsHidden;
            requestedEntity.BrandId = request.BrandId;
            requestedEntity.SubCategoryId = request.SubCategoryId;
            requestedEntity.ProductType = request.ProductType;

            if (request.Photos == null)
            {
                return requestedEntity.Id;
            }

            foreach (var photo in request.Photos)
            {
                requestedEntity.Photos.Add(await this.UploadImage(photo));
                requestedEntity.Photos.Remove("Microsoft.AspNetCore.Http.FormFile");
            }

            this.productsRepository.Update(requestedEntity);
            await this.productsRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }

        private async Task<string> UploadImage(IFormFile photo)
        {
            return await this.cloudinaryHelper.UploadImage(
                photo,
                name: $"{photo.Name}-product-item-shot");
        }
    }
}