using ETicaretAPI_V2.Application.Abstraction.Storage;
using ETicaretAPI_V2.Application.Features.Commands.CreateProduct;
using ETicaretAPI_V2.Application.Features.Queries.GetAllProduct;
using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Application.Repositories.FileRepositories;
using ETicaretAPI_V2.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Application.RequestParameters;

using ETicaretAPI_V2.Application.ViewModels.Products;
using ETicaretAPI_V2.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretAPI_V2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        readonly IStorageService _storageService;
        readonly IFileReadRepository _fileReadRepository;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IConfiguration _configuration;

        readonly IMediator _mediator;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
        {
           GetAllProductQueryResponse response=  await _mediator.Send(getAllProductQueryRequest);
           
            return Ok(response);
           
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response= await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)(HttpStatusCode.Created));
        }
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Products model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)(HttpStatusCode.OK));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
             var datas= await _storageService.UploadAsync( Request.Form.Files, "photo-images");
            Product product = await _productReadRepository.GetByIdAsync(id);
            //var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(data => new ProductImageFile()
            {
                FileName = data.fileName,
                Path = data.pathOrContainer,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }

            }).ToList()) ;
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductsImages(string id)
        {
            Product product = await _productReadRepository.Table.Include(p=>p.ProductImageFiles).FirstOrDefaultAsync(p=>p.Id==Guid.Parse(id));
            var images = product.ProductImageFiles.Select(p=>new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.Id,
                p.FileName
            });
            return Ok(images);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id,string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            ProductImageFile productImageFile= product.ProductImageFiles.FirstOrDefault(f => f.Id == Guid.Parse(imageId));
             product.ProductImageFiles.Remove(productImageFile);
            await _productImageFileWriteRepository.RemoveAsync(imageId);
            await _fileWriteRepository.SaveAsync();
            return Ok();
        }

    }
}
