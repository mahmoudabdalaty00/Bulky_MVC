using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.RepositoriesClasses
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private   ApplicationDbContext _context;
        private readonly IProductRepository _repository;

        public ProductRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
