using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data;

namespace RealEstateBE.Service.Abstract
{
    public class SaveContext
    {
        public readonly DataContext _dataContext;
        public SaveContext(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected bool SaveChanges()
        {
           return _dataContext.SaveChanges()>0;
        }
    }
}
