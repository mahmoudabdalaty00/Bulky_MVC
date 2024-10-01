using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Data;
using BulkyWeb.Models;
 
 
 

namespace Bulky.DataAccess.Repository.RepositoriesClasses
{
	public class CategoryRepository : Repository<Category> , ICategoryRepository
	{
		private   ApplicationDbContext _db; 
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
 
		public void Update(Category category)
		{
			_db.Categories.Update(category);
		}
	}
}
