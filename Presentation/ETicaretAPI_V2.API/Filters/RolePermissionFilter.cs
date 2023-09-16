using ETicaretAPI_V2.Application.Abstraction.Services;
using ETicaretAPI_V2.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System.Text.RegularExpressions;

//{Regex.Replace(_action.Definition, @"\s+", "")}
namespace ETicaretAPI_V2.API.Filters
{
    //Bu filter aspnet core pipelineda her istek geldiğinde işlenir
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //JWT TOKEN OLUŞTURURUKEN VERDİĞİMİZ CLAIMTYPE SONUCUDUNA USER'IN USERNAMEİNE ULAŞABİLİRİZ. (TOKEN HANDLER + PROGRAM.CS)
           var name  =  context.HttpContext.User.Identity?.Name;
            if (!string.IsNullOrEmpty(name) && name!="crklih")
            {
                //istek hangi actiona gidiyorsa onla ilgili bilgileri getiren ActionDescriptor sınıfın ActionDescriptor propertysini conteximizden çağırıyoruz
                //bizim controllerıza gelen istekleri yakalammız için özelleştirmemiz lazım bunun için as ile ControllerActionDescriptor sınıfı türünde olacağını belirtiyoruz

                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                // MethodInfo üzerinden ilgili actionun işartlenmiş olan attributlarına erişebiliriz. 
                // Ancak bu method bize attribute türünden geri dönüş yapacaktır ilgili attributeun propertylerine erişebilmek için as ile AuthorizeDefinitionAttribute türüne dönüştürmemiz gerekiyor
                var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                //üstte tanımladığımız attribute bize ilgili actionun typenı getirmediği için  HttpMethod attributeuda tanımlamamız gerekir.
                //sonuç olarak ilgili actionun typeını da yakalamış olacağız
                var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

                //veritabanında endpointleri sorgulamak için code'a dönüştürme
                var code = $"{(httpAttribute!=null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{Regex.Replace(attribute.Definition, @"\s+", "")}";

                var hasRole = await _userService.HasRolePermissionToEndpointAsync(name,code);

                if (!hasRole)
                {
                    //eğer ilgili kullanıcı role sahip değil ise 401 dönecektir
                    context.Result = new UnauthorizedResult();
                }
                else
                    await next();
            }
            else
                await next();
        }
    }
}
