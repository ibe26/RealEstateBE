using RealEstateEntities.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace RealEstateBE.Service
{
    public static class ServiceHelper
    {
        public static void MoveToBottom<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            T? entity = list.SingleOrDefault(predicate);
            if (entity != null)
            {
                list.Remove(entity);
                list.Add(entity);
            }
        }
    }
        
}
