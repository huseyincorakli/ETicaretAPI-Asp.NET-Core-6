using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public class Menu:BaseEntitiy
    {
        public string Name { get; set; }
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
