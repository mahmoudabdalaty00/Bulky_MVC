using Bulky.Models.Models;
 

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IOrderHeaderRepository : IRepository<OrderHeader>
	{
		void Update( OrderHeader  orderHeader);
	 
	}
}
