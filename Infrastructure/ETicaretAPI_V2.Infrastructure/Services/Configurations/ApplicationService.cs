using ETicaretAPI_V2.Application.Abstraction.Services.Configurations;
using ETicaretAPI_V2.Application.CustomAttributes;
using ETicaretAPI_V2.Application.DTOs.Configurations;
using ETicaretAPI_V2.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Reflection;
using DTO = ETicaretAPI_V2.Application.DTOs.Configurations;

namespace ETicaretAPI_V2.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);

            //Api içerisindeki controllerları çekme
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<Menu> menus = new();

            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    //controller içerisindeki [AuthorizeDefinition] attribute ile işaretlenmiş olanları yakalama
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                            //ilgili actionın tüm attributelarını yakalama
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                Menu menu = null;

                                //ilgili actionun  attributelarından typeı [AuthorizeDefinition] olanı değere atadık 
                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                {
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);
                                }

                                DTO.Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType),authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition,
                                };

                                //ilgili actionun  attributeların typelarından olan ve HttpMethodAttribute classından türemiş olan httpAttributeları değere atadık
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                {
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                }
                                else
                                {
                                    _action.HttpType = HttpMethods.Get;
                                }
                                menu.Actions.Add(_action);
                            }
                        }
                    }
                }
            }

            return menus;
        }
    }
}
