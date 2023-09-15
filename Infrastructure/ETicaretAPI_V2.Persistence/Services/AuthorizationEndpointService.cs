using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.Abstraction.Services.Configurations;
using ETicaretAPI_V2.Application.Repositories.EndpointRepositories;
using ETicaretAPI_V2.Application.Repositories.MenuRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IApplicationService _applicationServce;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole> _roleManager;
        public AuthorizationEndpointService(
            IApplicationService applicationServce,
            IEndpointReadRepository endpointReadRepository,
            IEndpointWriteRepository endpointWriteRepository,
            IMenuReadRepository menuReadRepository,
            IMenuWriteRepository menuWriteRepository,
            RoleManager<AppRole> roleManager
            )
        {
            _applicationServce = applicationServce;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string code, Type type, string menu)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            //veritabanında ilgili menu eğer yok ise menu oluşturduk
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu,
                };
                await _menuWriteRepository.AddAsync(_menu);
                await _menuWriteRepository.SaveAsync();
            }

            //ilgili menude bizim code'a karşılık gelen endpoint var mı ?
            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e=>e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            //eğer yok ise
            if (endpoint == null)
            {   //gelen menunün actionlarından gelen code ile eşleşen actionu alıyoruz
                var action = _applicationServce.GetAuthorizeDefinitionEndpoints(type).FirstOrDefault(m => m.Name == menu)?.Actions.FirstOrDefault(e => e.Code == code);
                endpoint = new()
                {
                    Id = Guid.NewGuid(),
                    Code = action.Code,
                    ActionType = action.ActionType,
                    Definition = action.Definition,
                    HttpType = action.HttpType,
                    Menu = _menu
                };
                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }
            else
            {
                foreach (var role in endpoint.Roles)
                {
                    endpoint.Roles.Remove(role);
                }
            }

            //roles parametresinden gelen değere göre rolleri çekiyoruz
            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            //ilgili endpointe rolleri atıyoruz.
            foreach (var role in appRoles)
            {
                endpoint.Roles.Add(role);
            }
            await _endpointWriteRepository.SaveAsync();

        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code,string menu)
        {
            Endpoint? endpoint =await _endpointReadRepository.Table.Include(e => e.Roles).Include(e=>e.Menu).FirstOrDefaultAsync(e => e.Code==code && e.Menu.Name== menu);
            if (endpoint!=null)
                return endpoint.Roles.Select(r => r.Name).ToList();
            
            else
                return null;
            
        }
    }
}
