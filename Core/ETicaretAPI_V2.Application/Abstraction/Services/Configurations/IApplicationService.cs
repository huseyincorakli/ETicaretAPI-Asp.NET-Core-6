using ETicaretAPI_V2.Application.DTOs.Configurations;

namespace ETicaretAPI_V2.Application.Abstraction.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
