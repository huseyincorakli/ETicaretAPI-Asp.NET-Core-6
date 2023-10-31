using ETicaretAPI_V2.Application.Repositories.CategoryRepositories;
using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ETicaretAPI_V2.Application.Features.Queries.Category.GetCategoryById
{
	internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, GetCategoryByIdQueryResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;
		
		public GetCategoryByIdQueryHandler(ICategoryReadRepository categoryReadRepository)
		{
			_categoryReadRepository = categoryReadRepository;
			
		}

		public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var options = new JsonSerializerOptions
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles
			};
			var datas = await _categoryReadRepository.GetByIdAsync(request.CategoryId);
			
			if (datas != null)
			{
				

				var datass = "";
				return new()
				{
					Datas = datass
				};
			}
			else
			{
				throw new Exception("Error");
			}
		}
	}
}
