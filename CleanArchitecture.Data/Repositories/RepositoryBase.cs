﻿using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infraestructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly StreamerDbContext _context;

        public RepositoryBase(StreamerDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
           _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null, bool disableTraking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTraking)
            {
                query = query.AsNoTracking();
            }
            if (string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }
            if (predicate!=null)
            {
                query = query.Where(predicate);
            }
            if (orderBy!=null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            List<Expression<Func<T, object>>> includes = null, 
            bool disableTraking = true)
        {
            IQueryable<T> query =_context.Set<T>();
            if (disableTraking)
            {
                query = query.AsNoTracking();
            }
            if (includes!=null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (predicate!=null)
            {
                query=query.Where(predicate);
            }
            if (orderBy!=null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetIdAsync(int id)
        {
           return await  _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
           await _context.SaveChangeAsync();
            return entity;
             
        }
    }
}
