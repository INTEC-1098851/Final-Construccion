using GenericRepository.Entities;
using GenericRepository.Extensions;
using GenericRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Repositories
{
    public abstract class EntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
          where TEntity : class, IEntity
          where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _collections;
        private readonly IHttpContextAccessor _contextAccessor;
        public EntityRepository(TContext context,IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._collections = context.Set<TEntity>();
            _contextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
       
        public EntityRepository(TContext context)
        {
            this._context = context;
            this._collections = context.Set<TEntity>();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            if (_contextAccessor != null)
                entity.CreatedBy = _contextAccessor.HttpContext.User.Identity.Name;
            _collections.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //public virtual async Task<TEntity> DeleteAsync(int id)
        //{
        //    var entity = await _collections.FindAsync(id);
        //    if (entity == null)
        //    {
        //        return entity;
        //    }

        //    _collections.Remove(entity);
        //    await _context.SaveChangesAsync();

        //    return entity;
        //}
        public virtual async Task<bool> DeleteAsync(int? id)
        {
            var entity = await _collections.FindAsync((int)id);
            if (entity == null)
            {
                return false;
            }
            if (_contextAccessor != null)
                entity.UpdatedBy = _contextAccessor.HttpContext.User.Identity.Name;
            await UpdateAsync(entity);

            _collections.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }


        public virtual async Task<TEntity> GetOneAsync(int? id)
        {
            return await _collections.FindAsync((int)id);
        }



        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_collections.AsEnumerable());
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var remoteEntity = await GetOneAsync(entity.Id);
            entity.CreatedBy = remoteEntity.CreatedBy;
            entity.CreatedDate= remoteEntity.CreatedDate;
            entity.UpdatedDate = DateTime.Now;
            if (_contextAccessor != null)
                entity.UpdatedBy= _contextAccessor.HttpContext.User.Identity.Name;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<bool> ChangeStatusAsync(TEntity entity)
        {
            var remoteEntity = await _collections.FindAsync(entity.Id);
            remoteEntity.StatusId = entity.StatusId;
            remoteEntity.UpdatedDate= DateTime.Now;
            if (_contextAccessor != null)
                remoteEntity.UpdatedBy = _contextAccessor.HttpContext.User.Identity.Name;
            //remoteEntity.UpdatedBy = entity.UpdatedBy;
            _context.Entry(remoteEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<SqlParameter>> ExecuteSqlAsync(string procedureName = null, List<SqlParameter> sqlParameters = null)
        {
            string sqlRaw = $"{procedureName} {sqlParameters.JoinSqlParameters()}";
            //int response;
            if (sqlParameters == null)
                await _context.Database.ExecuteSqlRawAsync(sqlRaw);
            else
                await _context.Database.ExecuteSqlRawAsync(sqlRaw, sqlParameters);
            return sqlParameters.Where(x => x.Direction == ParameterDirection.Output);
        }
        public virtual Task<List<TEntity>> ExecuteFromSqlRaw(string procedureName, List<SqlParameter> sqlParameters = null)
        {
            List<TEntity> result;
            if (sqlParameters == null)
                result = _collections.FromSqlRaw($"exec {procedureName}").ToList();
            else
                result = _collections.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
                  .ToList();

            return Task.FromResult(result);
        }
        //public virtual Task<List<type>> ExecuteFromSqlRaw(Type type, string procedureName, List<SqlParameter> sqlParameters = null)
        //{
        //    var typeColleciton = context.Set<type>();
        //    List<type> result;
        //    if (sqlParameters == null)
        //        result = typeColleciton.FromSqlRaw($"exec {procedureName}").ToList();
        //    else
        //        result = typeColleciton.FromSqlRaw($"exec {procedureName} {sqlParameters.JoinSqlParameters()}", sqlParameters?.ToArray())
        //          .ToList();

        //    return Task.FromResult(result);
        //}


    }

    //public class EFGenericRepository<TEntity> : IEFGenericRepository<TEntity> where TEntity : class, new()
    //{
    //    protected readonly CatadorDbContext _context;

    //    public EFGenericRepository(CatadorDbContext context)
    //    {
    //        _context = context ?? throw new ArgumentNullException(nameof(context));
    //    }

    //    public IEnumerable<TEntity> GetAll()
    //    {
    //        try
    //        {
    //            return __collections;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception($"Couldn't retrieve entities: {ex.Message}");
    //        }
    //    }

    //    public async Task<TEntity> AddAsync(TEntity entity)
    //    {
    //        if (entity == null)
    //        {
    //            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
    //        }

    //        try
    //        {
    //            await _context.AddAsync(entity);
    //            await _context.SaveChangesAsync();

    //            return entity;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
    //        }
    //    }

    //    public async Task<TEntity> UpdateAsync(TEntity entity)
    //    {
    //        if (entity == null)
    //        {
    //            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
    //        }

    //        try
    //        {
    //            _context.Entry(entity).State = EntityState.Modified;
    //            await _context.SaveChangesAsync();

    //            return entity;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
    //        }
    //    }
    //}
}
