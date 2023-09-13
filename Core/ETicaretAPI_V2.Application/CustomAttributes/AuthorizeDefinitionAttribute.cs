using ETicaretAPI_V2.Application.Enums;

namespace ETicaretAPI_V2.Application.CustomAttributes
{
    public class AuthorizeDefinitionAttribute:Attribute
    {
        public string Menu { get; set; }
        public string Definition { get; set; }
        public ActionType ActionType { get; set; }
    }
}
