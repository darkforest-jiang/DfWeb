using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DfHelper.EF.Base
{
    public interface IBaseService<TDbContext> 
        where TDbContext : DbContext 
    {
        #region 实体更新
        /// <summary>
        /// 添加单个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        Task<int> EntityAdd<Entity>(Entity T, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 添加多条实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        Task<int> EntityAdd<Entity>(List<Entity> T, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 修改单个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        Task<int> EntityEdit<Entity>(Entity T, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 修改多个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        Task<int> EntityEdit<Entity>(List<Entity> T, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 根据主键更新实体对象
        /// </summary>
        /// <param name="T"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> EntityEdit<Entity>(Entity T, CancellationToken cancellationToken = default, params object[] param) where Entity : class;

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        Task<int> EntityDelete<Entity>(Entity T, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 删除多个实体数据
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        Task<int> EntityDelete<Entity>(List<Entity> T, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 按条件删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> EntityDelete<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;
        #endregion

        #region 实体查询
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Entity?> FirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="isAsc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Entity?> FirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, object>> order, bool isAsc = true, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        Task<Entity?> FirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default, params IOrderByExpression<Entity>[] orders) where Entity : class;

        /// <summary>
        /// 是否存在一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 获取实体记录条数
        /// </summary>
        /// <returns></returns>
        Task<int> EntityCount<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IQueryable<Entity> EntityQuery<Entity>() where Entity : class;

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<Entity> EntityQuery<Entity>(Expression<Func<Entity, bool>> where) where Entity : class;

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<Entity>> EntityQuery<Entity>(CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IList<Entity>> EntityQuery<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;

        #endregion

        #region 无缓存查询
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Entity?> NonCacheFirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="isAsc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Entity?> NonCacheFirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, object>> order, bool isAsc = true, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        Task<Entity?> NonCacheFirstOrDefault<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default, params IOrderByExpression<Entity>[] orders) where Entity : class;

        /// <summary>
        /// 获取实体记录条数
        /// </summary>
        /// <returns></returns>
        Task<int> NonCacheEntityCount<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IQueryable<Entity> NonCacheEntityQuery<Entity>() where Entity : class;

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<Entity> NonCacheEntityQuery<Entity>(Expression<Func<Entity, bool>> where) where Entity : class;

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<Entity>> NonCacheEntityQuery<Entity>(CancellationToken cancellationToken = default) where Entity : class;

        /// <summary>
        /// 按条件获取数据
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IList<Entity>> NonCacheEntityQuery<Entity>(Expression<Func<Entity, bool>> where, CancellationToken cancellationToken = default) where Entity : class;
        #endregion

    }
}
