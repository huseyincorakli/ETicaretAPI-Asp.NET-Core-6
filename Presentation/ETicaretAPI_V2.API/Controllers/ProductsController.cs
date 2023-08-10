using ETicaretAPI_V2.Application.Abstraction.Storage;
using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Application.Repositories.FileRepositories;
using ETicaretAPI_V2.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using ETicaretAPI_V2.Application.RequestParameters;

using ETicaretAPI_V2.Application.ViewModels.Products;
using ETicaretAPI_V2.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.CreateDate,
                p.UpdatedDate
            }).Skip(pagination.Size * pagination.Page).Take(pagination.Size);


            return Ok(new { totalCount, products });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
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
    }
}
