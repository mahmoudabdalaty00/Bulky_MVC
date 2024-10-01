using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Data;
using BulkyWeb.Models;
using System.Linq.Expressions;


namespace Bulky.DataAccess.Repository.RepositoriesClasses
{
	public class OrderDetailsRepository : Repository<OrderDetails> , IOrderDetailsRepository
    {
		private ApplicationDbContext _db; 
		public OrderDetailsRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

        public void Update(OrderDetails order)
        {
            _db.OrderDetails.Update(order);
        }
    }
}
