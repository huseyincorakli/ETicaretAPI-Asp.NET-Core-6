using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Consts;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.Enums;
using ETicaretAPI_V2.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI_V2.Application.Features.Commands.Product.CreateProductDescription;
using ETicaretAPI_V2.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI_V2.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI_V2.Application.Features.Commands.Product.UpdateProductStockFromQr;
using ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETicaretAPI_V2.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI_V2.Application.Features.Queries.Product.GetBestSellingProduct;
using ETicaretAPI_V2.Application.Features.Queries.Product.GetByIdProduct;
using ETicaretAPI_V2.Application.Features.Queries.Product.GetLowStockProduct;
using ETicaretAPI_V2.Application.Features.Queries.Product.GetProductsByCategory;
using ETicaretAPI_V2.Application.Features.Queries.ProductImageFile.GetProductImages;
using ETicaretAPI_V2.Application.Repositories.DailySaleRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProductsController : ControllerBase
	{
		readonly IMediator _mediator;
		readonly IProductService _productService;
		readonly IProductReadRepository _productReadRepository;
		readonly IDailySaleReadRepository _dailySaleReadRepository;
		public ProductsController(IMediator mediator, IProductService productService, IProductReadRepository productReadRepository, IDailySaleReadRepository dailySaleReadRepository)
		{
			_mediator = mediator;
			_productService = productService;
			_productReadRepository = productReadRepository;
			_dailySaleReadRepository = dailySaleReadRepository;
		}



		[HttpGet("qrCode/{productId}")]
		public async Task<IActionResult> GetQRCodeToProduct([FromRoute] string productId)
		{
			var data = await _productService.QRCodeToProductAsync(productId);
			return File(data, "image/png");
		}
		[HttpPut("qrCode")]
		public async Task<IActionResult> UpdateProductStockFromQr(UpdateProductStockFromQrCommandRequest updateProductStockFromQrCommandRequest)
		{
			UpdateProductStockFromQrCommandResponse response = await _mediator.Send(updateProductStockFromQrCommandRequest);
			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
		{
			GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);

			return Ok(response);

		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
		{
			GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
			return Ok(response);
		}


		[HttpPost]
		//[Authorize(AuthenticationSchemes = "Admin")]
		//[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
		public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
		{
			CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
			return Ok(response);
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> CreateProductDescription(CreateProductDescriptionCommandRequest createProductDescriptionCommandRequest)
		{
			CreateProductDescriptionCommandResponse response = await _mediator.Send(createProductDescriptionCommandRequest);
			return Ok(response);
		}

		[HttpPut]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
		public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
		{
			UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
			return (Ok());
		}


		[HttpDelete("{Id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Remove Product")]
		public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
		{
			RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
			return Ok();
		}

		[HttpPost("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Upload Product Image File")]
		public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
		{
			uploadProductImageCommandRequest.Files = Request.Form.Files;
			UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
			return Ok();
		}


		[HttpGet("[action]/{Id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Products Images")]
		public async Task<IActionResult> GetProductsImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
		{
			List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
			return Ok(response);
		}


		[HttpDelete("[action]/{Id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Remove Product Image File")]
		public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
		{
			removeProductImageCommandRequest.ImageId = imageId;
			RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
			return Ok();
		}

		[HttpGet("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Change Showcase Product Image File")]
		public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseCommandRequest changeShowcaseCommandRequest)
		{
			ChangeShowcaseCommandResponse response = await _mediator.Send(changeShowcaseCommandRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetProductsByCategory([FromQuery]GetProductsByCategoryQueryRequest getProductsByCategoryQueryRequest)
		{
			var response = await _mediator.Send(getProductsByCategoryQueryRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetLowStockProducts([FromQuery] GetLowStockProductQueryRequest getLowStockProductQueryRequest)
		{
			GetLowStockProductQueryResponse response = await _mediator.Send(getLowStockProductQueryRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetBestSellingProducts([FromQuery] GetBestSellingProductQueryRequest getBestSellingProductQueryRequest)
		{
			GetBestSellingProductQueryResponse response = await _mediator.Send(getBestSellingProductQueryRequest);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetDailySale(int year,int mounth,int day)
		{
			var data= await _dailySaleReadRepository.GetDailySale (new DateTime(year, day, mounth, 0, 0, 0, DateTimeKind.Utc));
			return Ok(data);
		}

	}
}
