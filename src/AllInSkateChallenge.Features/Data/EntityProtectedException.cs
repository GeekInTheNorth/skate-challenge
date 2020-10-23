using System;

namespace AllInSkateChallenge.Features.Data
{
    public class EntityProtectedException : Exception
    {
        public EntityProtectedException(Type entityType, string entityId) : base($"Entity of type {entityType.Name} with the Id of {entityId} is a protected record.")
        {
        }

        public EntityProtectedException(Type entityType, Guid entityId) : this(entityType, entityId.ToString())
        {
        }
    }
}
