namespace Fashionista.Application.Products.Commands.AddReview
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, int>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public AddReviewCommandHandler(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (request.Rating < 0 || request.Rating > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(request));
            }

            var requestedEntity = await this.productsRepository
                                      .All()
                                      .SingleOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken)
                                  ?? throw new NotFoundException(nameof(Product), request.ProductId);

            requestedEntity.Reviews.Add(new Review
            {
                Rating = request.Rating,
            });

            this.productsRepository.Update(requestedEntity);
            await this.productsRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.Id;
        }
    }
}
