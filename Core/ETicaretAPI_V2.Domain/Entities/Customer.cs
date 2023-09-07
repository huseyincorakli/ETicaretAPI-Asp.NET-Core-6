using ETicaretAPI_V2.Domain.Entities.Common;

namespace ETicaretAPI_V2.Domain.Entities
{
    public  class Customer:BaseEntitiy
    {
        public string Name { get; set; }
        //public ICollection<Order> Orders { get; set; }  
    }
}
