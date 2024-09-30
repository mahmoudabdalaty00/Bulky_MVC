using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Data;
using BulkyWeb.Models;

namespace Bulky.DataAccess.Repository.RepositoriesClasses
{
	public class CompanyRepository : Repository<Company>, ICompanyRepository
	{

		private ApplicationDbContext _db;
		public CompanyRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Company Company)
		{
			_db.Companies.Update(Company);
		}
	}
}
