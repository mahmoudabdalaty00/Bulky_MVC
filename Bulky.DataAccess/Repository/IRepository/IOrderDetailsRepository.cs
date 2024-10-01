using Bulky.Models.Models;
 
namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IOrderDetailsRepository : IRepository<OrderDetails>
	{
		void Update(OrderDetails  order);
	 
	}
}
