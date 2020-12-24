﻿using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopich.Repositories.interfaces
{
    public interface IReview
    {
        public Task<Review[]> GetAll();
        public void Create(Review entity);
        public Task<Review> GetById(int id);
        public void Update(Review entity);
        public void Delete(int id);
        public void Save();
        public IQueryable<Review> Include(params Expression<Func<Review, object>>[] includeProperties);
        public bool Exists(int id);
    }
}