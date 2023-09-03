using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseCommandRequest:IRequest<ChangeShowcaseCommandResponse>
    {
        public string ImageId { get; set; }
        public string ProductId  { get; set; }
    }
}
