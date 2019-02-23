using Hein.Framework.Repository.Criterion;
using Hein.Framework.Repository.SQL;
using System.Collections.Generic;
using System.Linq;

namespace Hein.Framework.Repository.Entity
{
    public class EntityRepository<T> : Repository<T> where T : IEntity
    {
        public EntityRepository(IRepositoryContext context) : base(context)
        { }

        private SqlTransactionQuery<T> _transaction = RepositoryTransaction.Of<T>();

        public override T Save(T entity)
        {
            entity.ExecuteBeforeSave();

            if (entity.GetId() == 0)
            {
                _transaction = _transaction.Insert(entity);
                entity.SetId(
                    this.Context.Query<long>(_transaction.SQL).FirstOrDefault());
            }
            else
            {
                _transaction = _transaction.Update(entity);
                this.Context.Execute(_transaction.SQL);
            }

            entity.ExecuteAfterSave();
            return entity;
        }

        public override void Delete(T entity)
        {
            if (entity is ISoftDelete)
            {
                ((ISoftDelete)entity).IsDeleted = true;
                Save(entity);
            }
            else
            {
                _transaction = _transaction.Delete(entity);
                this.Context.Execute(_transaction.SQL);
            }
        }

        public override IEnumerable<T> Find(IQueryCriteria<T> query)
        {
            return this.Context.Query<T>(query.SQL);
        }

        public override IEnumerable<T> GetAll()
        {
            return Find(QueryOver.Of<T>());
        }
    }
}
