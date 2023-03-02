using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfHelper.EF.Base
{
    public interface IOrderByExpression<Entity> where Entity : class
    {
        IOrderedQueryable<Entity> ApplyOrderBy(IQueryable<Entity> query);

        IOrderedQueryable<Entity> ApplyThenBy(IOrderedQueryable<Entity> query);
    }
}
