using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace TravelLine.Food.Infrastructure.Common
{
    public class EFGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFGenericRepository( DbContext context )
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return QueryReadOnly().ToList();
        }

        public TEntity Get( int id )
        {
            return _dbSet.Find( id );
        }

        public void Save( TEntity item )
        {
            _context.Set<TEntity>().AddOrUpdate( item );
            _context.SaveChanges();
        }

        public void Save( List<TEntity> items )
        {
            foreach ( TEntity item in items )
            {
                _context.Set<TEntity>().AddOrUpdate( item );
            }

            _context.SaveChanges();
        }

        public void Remove( int id )
        {
            TEntity entity = _dbSet.Find( id );
            if( entity != null )
            {
                Remove( entity );
            }
        }

        public void Remove( TEntity item )
        {
            _dbSet.Remove( item );
            _context.SaveChanges();
        }

        public void Remove( List<TEntity> items )
        {
            _dbSet.RemoveRange( items );
            _context.SaveChanges();
        }

        protected virtual IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable<TEntity>();
        }

        protected virtual IQueryable<TEntity> QueryReadOnly()
        {
            return _dbSet.AsNoTracking();
        }

        protected void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
