using ETicaretAPI_V2.Application.Repositories.CustomerRepositories;
using ETicaretAPI_V2.Application.Repositories.OrderRepositories;
using ETicaretAPI_V2.Application.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI_V2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var x = _productReadRepository.GetAll();
            return Ok(x);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product= await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
