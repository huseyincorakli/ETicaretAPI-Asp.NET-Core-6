using ETicaretAPI_V2.Application.Enums;

namespace ETicaretAPI_V2.Application.DTOs.Configurations
{
    public class Action
    {
        public string  ActionType { get; set; }
        public string HttpType  { get; set; }
        public string Definition { get; set; }
        public string  Code { get; set; }
    }
}
