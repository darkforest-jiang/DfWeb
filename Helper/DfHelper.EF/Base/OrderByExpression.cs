using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DfHelper.EF.Base
{
    public class OrderByExpression<Entity, OrderBy> : IOrderByExpression<Entity> where Entity : class
    {
        
        private Expression<Func<Entity, OrderBy>> _expression;
        private bool _descending;

        public OrderByExpression(Expression<Func<Entity, OrderBy>> expression, bool descending = false)
        {
            _expression = expression;
            _descending = descending;
        }

        public IOrderedQueryable<Entity> ApplyOrderBy(IQueryable<Entity> query)
        {
            if (_descending)
            {
                return query.OrderByDescending(_expression);
            }
            else
            {
                return query.OrderBy(_expression);
            }
        }

        public IOrderedQueryable<Entity> ApplyThenBy(IOrderedQueryable<Entity> query)
        {
            if (_descending)
            {
                return query.ThenByDescending(_expression);
            }
            else
            {
                return query.ThenBy(_expression);
            }
        }
    }
}
