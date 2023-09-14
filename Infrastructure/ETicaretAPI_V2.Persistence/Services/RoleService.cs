using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI_V2.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
            
            IdentityResult result =  await _roleManager.CreateAsync(new AppRole() {Id=Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string id)
        {
            AppRole role =  await _roleManager.FindByIdAsync(id);
            IdentityResult result  = await _roleManager.DeleteAsync(role); 
            return result.Succeeded;
        }

        public (object,int) GetAllRoles(int page, int size)
        {
            var query = _roleManager.Roles;
           var data= query.Skip(page*size).Take(size).Select(o =>new
           {
               o.Id,
               o.Name,
           });

            return (data, query.Count());
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
            string role = await _roleManager.GetRoleIdAsync(new AppRole() { Id=id});
            return (id, role);
        }

        public async Task<bool> UpdateRole(string id, string name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result  = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
