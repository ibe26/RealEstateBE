﻿using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data.Concrete;
using RealEstateEntities.Entities;
using RealEstateEntities.Entities.DTOs.User;
using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Concrete
{
    public class UserDal : RepositoryReadWrite<User>, IUserDal
    {
        public async Task<bool> Any(Expression<Func<User, bool>> predicate)
        {
            return await base._entities.AnyAsync(predicate);
        }

        

        public override async Task<User?> GetByIdAsync(object id)
        {
            SaveChanges();
            return await base._entities.Include(u=>u.OwnedProperties).Include(u=>u.ListedProperties).SingleOrDefaultAsync(u=>u.UserID==new Guid(id.ToString()!));
        }
       

    }
}
