using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Data;


namespace Bulky.DataAccess.Repository.RepositoriesClasses
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
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

        public void UpdateStatus(int id, string orderStatus, string? PaymentStatus = null)
        {
            var OrderfromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);

            if (OrderfromDb != null)
            {
                OrderfromDb.OrderStatus = orderStatus;

                if (!string.IsNullOrWhiteSpace(PaymentStatus))
                {
                    OrderfromDb.PaymentStatus = PaymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var OrderfromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
           
            if (!string.IsNullOrEmpty(sessionId))
            {
                OrderfromDb.SessionId = sessionId;

            } 
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                OrderfromDb.PaymentIntentId = paymentIntentId;
                OrderfromDb.PaymentDate = DateTime.Now;

            }
        }
    }
}
