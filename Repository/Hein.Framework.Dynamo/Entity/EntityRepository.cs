using Hein.Framework.Dynamo.Command;
using Hein.Framework.Dynamo.Criterion;
using System;
using System.Collections.Generic;

namespace Hein.Framework.Dynamo.Entity
{
    public class EntityRepository<T> : Repository<T> where T : IEntity
    {
        public EntityRepository(IRepositoryContext context) : base(context)
        { }

        private DynamoTransactionCommand<T> _transaction = RepositoryTransaction.Of<T>();

        public override T Save(T entity)
        {
            entity.ExecuteBeforeSave();

            if (entity.GetId() == default(Guid))
            {
                entity.SetId(Guid.NewGuid());
                _transaction = _transaction.Insert(entity);
                this.Context.Execute(_transaction.DynamoCommand);
            }
            else
            {
                _transaction = _transaction.Update(entity);
                this.Context.Execute(_transaction.DynamoCommand);
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
                this.Context.Execute(_transaction.DynamoCommand);
            }
        }

        public override IEnumerable<T> Find(IQueryCriteria<T> query)
        {
            return this.Context.Query<T>(query.DynamoCommand);
        }

        public override IEnumerable<T> GetAll()
        {
            return Find(QueryOver.Of<T>());
        }
    }
}
