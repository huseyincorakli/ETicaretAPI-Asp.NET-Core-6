using ETicaretAPI_V2.Application.Abstractions.Hubs;
using ETicaretAPI_V2.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductTagRepositories;
using ETicaretAPI_V2.Domain.Entities;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductHubService _productHubService;
        readonly ICategoryReadRepository _categoryReadRepository;


		public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService, ICategoryReadRepository categoryReadRepository)
		{
			_productWriteRepository = productWriteRepository;
			_productHubService = productHubService;
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
			var category = await _categoryReadRepository.GetByIdAsync(request.CategoryId);
            if (category!=null && category.IsActive==true)
			{
				await _productWriteRepository.AddAsync(new()
				{
					Name = request.Name,
					Price = request.Price,
					Stock = request.Stock,
					Desciription = request.Description,
					CategoryId=Guid.Parse(request.CategoryId)
				});
				await _productWriteRepository.SaveAsync();
				await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
				return new();
			}
			else
			{
				throw new Exception("Category is not active");
			}
           
            
            
        }
    }
}