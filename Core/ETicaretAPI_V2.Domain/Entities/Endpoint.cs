using ETicaretAPI_V2.Domain.Entities.Common;
using ETicaretAPI_V2.Domain.Entities.Identity;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class Endpoint : BaseEntitiy
    {
        public Endpoint()
        {
            Roles = new HashSet<AppRole>();
        }
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }

        public Menu Menu { get; set; }
        public ICollection<AppRole> Roles { get; set; }
    }
}
