using System;
using System.Data.Entity;
using System.Linq;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.Contexts;

namespace ModernStore.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ModernStoreDataContext _context;
        public ProductRepository(ModernStoreDataContext context)
        {
            
        }
        public Product Get(Guid id)
        {
            return _context
                .Products
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
