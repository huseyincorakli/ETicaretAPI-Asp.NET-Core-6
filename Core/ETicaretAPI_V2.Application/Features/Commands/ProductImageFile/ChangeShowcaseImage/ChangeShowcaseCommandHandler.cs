using ETicaretAPI_V2.Application.Repositories.ProductImageFileRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseCommandHandler : IRequestHandler<ChangeShowcaseCommandRequest, ChangeShowcaseCommandResponse>
    {
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowcaseCommandResponse> Handle(ChangeShowcaseCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table.Include(p => p.Products).SelectMany(p => p.Products, (pif, p) => new
            {
                pif,
                p
            });


            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);

            if (data != null)
            {
                data.pif.Showcase = false;
            }
            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));

            if (image != null)
            {
                image.pif.Showcase = true;
            }
            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
