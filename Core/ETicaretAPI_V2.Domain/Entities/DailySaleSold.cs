using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Domain.Entities
{
	public class DailySaleSold
	{
		public Guid Id { get; set; }
		public DateTime sale_date { get; set; }
		public int total_quantity_sold { get; set; }

	}
}
