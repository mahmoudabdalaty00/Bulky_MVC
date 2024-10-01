using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Data;
using BulkyWeb.Models;
 

namespace Bulky.DataAccess.Repository.RepositoriesClasses
{
	public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRepository
    {
		private ApplicationDbContext _db; 
		public OrderHeaderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
 
		public void Update(OrderHeader orderHeader)
		{
			_db.OrderHeaders.Update(orderHeader);
		}
 
    }
}
