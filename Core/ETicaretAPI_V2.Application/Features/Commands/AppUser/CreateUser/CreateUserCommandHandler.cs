using MediatR;
using Microsoft.AspNetCore.Identity;
using AU = ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<AU.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AU.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           IdentityResult result= await _userManager.CreateAsync(new AU.AppUser
            {
                Id=Guid.NewGuid().ToString(),
                NameSurname = request.NameSurname,
                Email = request.Email,
                UserName = request.Username,

            }, request.Password);
            CreateUserCommandResponse response = new CreateUserCommandResponse
            {
                Succeeded= result.Succeeded,
            };
            if (result.Succeeded)
            {
                response.Message = "Kullanıcı kaydı başarılı";
            }
            else
            {
                foreach (var errors in result.Errors)
                {
                    response.Message += $"{errors.Code} - {errors.Description} \n ";
                }
            }

            return response;
        }
    }
}
