using Bulky.Models.Models;
using BulkyWeb.Models;
 

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface ICompanyRepository : IRepository<Company>
	{
		void Update(Company Company);
	}
}
