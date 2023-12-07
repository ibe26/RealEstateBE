﻿using Microsoft.EntityFrameworkCore;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Data.Concrete;
using RealEstateBE.Entities;
using RealEstateBE.Entities.DTOs.User;
using System.Linq.Expressions;

namespace RealEstateBE.Dal.Concrete
{
    public class UserDal : RepositoryReadWrite<User>, IUserDal
    {
        public async Task<bool> AnyUser(Expression<Func<User, bool>> predicate)
        {
           return await base._entities.AnyAsync(predicate);
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await base._entities.ToListAsync();
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await base._entities.Include(u=>u.Properties).SingleOrDefaultAsync(u=>u.UserID==id);
        }

    }
}
