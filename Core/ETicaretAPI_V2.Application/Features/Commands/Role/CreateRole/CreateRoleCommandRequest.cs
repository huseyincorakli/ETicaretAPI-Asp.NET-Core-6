using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Role.CreateRole
{
    public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
    {
        public string  Name { get; set; }
    }
}
