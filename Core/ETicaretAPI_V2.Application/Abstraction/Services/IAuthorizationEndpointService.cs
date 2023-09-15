namespace ETicaretAPI_V2.Application.Abstraction.Services
{
    public interface IAuthorizationEndpointService
    {
        public Task AssignRoleEndpointAsync(string[] roles, string code,Type type,string menu);
        public Task<List<string>> GetRolesToEndpointAsync(string code,string menu);
    }
}
