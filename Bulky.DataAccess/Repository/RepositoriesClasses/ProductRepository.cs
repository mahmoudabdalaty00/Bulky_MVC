using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Data;

namespace Bulky.DataAccess.Repository.RepositoriesClasses
{
	internal class ProductRepository : Repository<Product>, IProductRepository
	{
		private ApplicationDbContext _context;
		private readonly IProductRepository _repository;

		public ProductRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(Product product)
		{
			var objfromDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
			if (objfromDb != null)
			{
				objfromDb.ListPrice = product.ListPrice;
				objfromDb.Price = product.Price;
				objfromDb.Price100 = product.Price100;
				objfromDb.Price50 = product.Price50;
				objfromDb.Author = product.Author;
				objfromDb.Description = product.Description;
				objfromDb.ISBM = product.ISBM;
				objfromDb.CategoryId = product.CategoryId;
				objfromDb.Title = product.Title;

				if (objfromDb.ImageURL != null)
				{
					objfromDb.ImageURL = product.ImageURL;
				}

			}
		}
	}
}
