using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse>
    {
        public string  Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
