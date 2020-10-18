using System;

namespace AllInSkateChallenge.Features.Data
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType, string entityId) : base($"An entity of type {entityType.Name} does not exist with the Id of {entityId}.")
        {
        }

        public EntityNotFoundException(Type entityType, Guid entityId) : this(entityType, entityId.ToString())
        {
        }
    }
}
