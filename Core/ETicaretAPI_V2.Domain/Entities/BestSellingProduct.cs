using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class BestSellingProduct
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int QuantitySold { get; set; }
	}
}
