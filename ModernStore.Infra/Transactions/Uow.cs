using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Infra.Contexts;

namespace ModernStore.Infra.Transactions
{
    public class Uow : IUow
    {
        private readonly ModernStoreDataContext _context;

        public Uow(ModernStoreDataContext conext)
        {
            _context = conext;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            //Do nothing :)
        }
    }
}
