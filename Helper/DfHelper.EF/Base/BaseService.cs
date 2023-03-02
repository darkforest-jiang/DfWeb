using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DfHelper.EF.Base
{
    public class BaseService<TDbContext> : IBaseService<TDbContext> 
        where TDbContext : DbContext
    {

        /// <summary>
        /// 实体数据上下文
        /// </summary>
        public TDbContext _DbSet { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseService(TDbContext dbContext)
        {
            _DbSet = dbContext;

            //_DbSet.Configuration.ProxyCreationEnabled = false;
            //_DbSet.Configuration.LazyLoadingEnabled = false;
        }

        #region 实体更新
        /// <summary>
        /// 添加单个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<int> EntityAdd<Entity>(Entity T, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                _DbSet.Entry(T).State = EntityState.Added;
                return await _DbSet.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 添加多条实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<int> EntityAdd<Entity>(List<Entity> T, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                _DbSet.Set<Entity>().AddRange(T);
                return await _DbSet.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 修改单个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<int> EntityEdit<Entity>(Entity T, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                _DbSet.Set<Entity>().Attach(T);
                _DbSet.Entry(T).State = EntityState.Modified;
                return await _DbSet.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据主键更新实体对象
        /// </summary>
        /// <param name="T"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> EntityEdit<Entity>(Entity T, CancellationToken cancellationToken = default, params object[] param) where Entity : class
        {
            try
            {
                var entity = await _DbSet.Set<Entity>().FindAsync(param);
                if(entity == null)
                {
                    throw new Exception("该实体信息未找到");
                }
                _DbSet.Entry(entity).CurrentValues.SetValues(T);
                return await _DbSet.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 修改多个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<int> EntityEdit<Entity>(List<Entity> T, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                foreach (Entity p in T)
                {
                    _DbSet.Set<Entity>().Attach(p);
                    _DbSet.Entry(p).State = EntityState.Modified;
                }
                return await _DbSet.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<int> EntityDelete<Entity>(Entity T, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                _DbSet.Set<Entity>().Attach(T);
                _DbSet.Entry(T).State = EntityState.Deleted;
                return await _DbSet.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 按条件删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> EntityDelete<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                IQueryable<Entity> list = _DbSet.Set<Entity>().Where<Entity>(where);
                if (list.Count() > 0)
                {
                    foreach (Entity item in list)
                    {
                        _DbSet.Entry(item).State = EntityState.Deleted;
                    }
                    return await _DbSet.SaveChangesAsync(cancellationToken);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除多个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<int> EntityDelete<Entity>(List<Entity> T, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                foreach (Entity p in T)
                {
                    _DbSet.Set<Entity>().Attach(p);
                    _DbSet.Entry(p).State = EntityState.Deleted;
                }
                return await _DbSet.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region 实体查询
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Entity?> FirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                var entity = await _DbSet.Set<Entity>().Where(where).FirstOrDefaultAsync(cancellationToken);
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="isAsc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Entity?> FirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, object>> order, bool isAsc = true, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                var query = _DbSet.Set<Entity>().Where(where);
                if(isAsc)
                {
                    query = query.OrderBy(order);
                }
                else
                {
                    query = query.OrderByDescending(order);
                }
                var entity = await query.FirstOrDefaultAsync(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public async Task<Entity?> FirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default, params IOrderByExpression<Entity>[] orders) where Entity : class
        {
            try
            {
                var query = _DbSet.Set<Entity>().Where(where);

                if(orders != null)
                {
                    query = GetOrderbyResult(query, orders);
                }

                var entity = await query.FirstOrDefaultAsync(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 是否存在一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                return await _DbSet.Set<Entity>().AnyAsync(where, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取实体记录条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> EntityCount<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                return await _DbSet.Set<Entity>().CountAsync(where, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IQueryable<Entity> EntityQuery<Entity>() where Entity : class
        {
            try
            {
                return _DbSet.Set<Entity>().AsQueryable();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<Entity> EntityQuery<Entity>(Expression<Func<Entity, bool>> where) where Entity : class
        {
            try
            {
                return _DbSet.Set<Entity>().Where(where);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<Entity>> EntityQuery<Entity>(CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                return await EntityQuery<Entity>().ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IList<Entity>> EntityQuery<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                return await EntityQuery<Entity>(where).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region 无缓存查询
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Entity?> NonCacheFirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                var entity = await _DbSet.Set<Entity>().Where(where).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="isAsc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Entity?> NonCacheFirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, object>> order, bool isAsc = true, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                var query = _DbSet.Set<Entity>().Where(where).AsNoTracking();
                if (isAsc)
                {
                    query = query.OrderBy(order);
                }
                else
                {
                    query = query.OrderByDescending(order);
                }
                var entity = await query.FirstOrDefaultAsync(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public async Task<Entity?> NonCacheFirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default, params IOrderByExpression<Entity>[] orders) where Entity : class
        {
            try
            {
                var query = _DbSet.Set<Entity>().Where(where).AsNoTracking();

                if (orders != null)
                {
                    query = GetOrderbyResult(query, orders);
                }

                var entity = await query.FirstOrDefaultAsync(cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取实体记录条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> NonCacheEntityCount<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                return await _DbSet.Set<Entity>().AsNoTracking().CountAsync(where, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IQueryable<Entity> NonCacheEntityQuery<Entity>() where Entity : class
        {
            try
            {
                return _DbSet.Set<Entity>().AsNoTracking().AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<Entity> NonCacheEntityQuery<Entity>(Expression<Func<Entity, bool>> where) where Entity : class
        {
            try
            {
                return _DbSet.Set<Entity>().Where(where).AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<Entity>> NonCacheEntityQuery<Entity>(CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                return await NonCacheEntityQuery<Entity>().ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IList<Entity>> NonCacheEntityQuery<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class
        {
            try
            {
                return await NonCacheEntityQuery<Entity>(where).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        /// <summary>
        /// 设置查询结果排序
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        private IQueryable<Entity> GetOrderbyResult<Entity>(IQueryable<Entity> query, params IOrderByExpression<Entity>[] orders) where Entity : class
        {
            try
            {
                IOrderedQueryable<Entity>? output = null;
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        if (output == null)
                        {
                            output = order.ApplyOrderBy(query);
                        }
                        else
                        {
                            output = order.ApplyThenBy(output);
                        }
                    }
                }
                return output ?? query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
