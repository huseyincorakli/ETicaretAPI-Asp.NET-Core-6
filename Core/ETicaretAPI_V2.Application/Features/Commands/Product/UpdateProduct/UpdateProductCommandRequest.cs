using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandRequest:IRequest<UpdateProductCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ShortDesciription { get; set; }
        public string Brand { get; set; }
        public string[] Spesification { get; set; }
    }
}
