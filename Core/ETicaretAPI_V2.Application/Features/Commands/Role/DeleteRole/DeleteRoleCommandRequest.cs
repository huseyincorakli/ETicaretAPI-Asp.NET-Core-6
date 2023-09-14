using MediatR;

namespace ETicaretAPI_V2.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
    {
        public string  Id { get; set; }
    }
}
