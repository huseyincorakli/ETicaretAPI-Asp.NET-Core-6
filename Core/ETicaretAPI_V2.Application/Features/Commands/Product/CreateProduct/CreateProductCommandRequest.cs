using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string  Description { get; set; }
        public string CategoryId { get; set; }
        public string Brand { get; set; }
        public string  ShortDesciription { get; set; }
        public string[] Specifications { get; set; }
    }
}
