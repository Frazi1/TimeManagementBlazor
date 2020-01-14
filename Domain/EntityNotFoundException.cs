using System;

namespace Domain
{
    public class EntityNotFoundException : Exception
    {
        public int Id { get; }

        protected EntityNotFoundException(int id, string message) 
            : base(message)
        {
            Id = id;
        }
    }
    public class EntityNotFoundException<TEntity> : EntityNotFoundException
    {
        public EntityNotFoundException(int id)
            : base(id, $"Entity {typeof(TEntity).FullName} with id {id} is not found.")
        {
        }
    }
}