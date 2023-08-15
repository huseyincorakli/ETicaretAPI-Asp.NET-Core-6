using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using P = ETicaretAPI_V2.Domain.Entities;
namespace ETicaretAPI_V2.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {

        IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
           P.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
           return new GetByIdProductQueryResponse { 
               Name= product.Name,
               Price= product.Price,
               Stock= product.Stock
           };
        }
    }
}
