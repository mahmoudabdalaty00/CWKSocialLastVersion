using System;

namespace Domain.Models.BaseEntities
{
    /// <summary>
    /// Abstract base class for all domain entities with audit and soft-delete capabilities
    /// </summary>
    /// <typeparam name="TId">The type of the entity's primary key</typeparam>
    public abstract class BaseEntity<TId> where TId : notnull
    {
        /// <summary>
        /// Primary key - initialized only by derived classes
        /// </summary>
        public TId Id { get; protected set; } = default!;

        /// <summary>
        /// UTC timestamp when the entity was created
        /// </summary>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// UTC timestamp when the entity was last modified (null if never updated)
        /// </summary>
        public DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Indicates whether the entity is soft-deleted
        /// </summary>
        public bool IsDeleted { get; protected set; }

        /// <summary>
        /// UTC timestamp when the entity was soft-deleted (null if not deleted)
        /// </summary>
        public DateTime? DeletedAt { get; protected set; }

        /// <summary>
        /// Initialize the entity with audit timestamps (called by factory methods)
        /// </summary>
        protected void InitializeAudit()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null; // Not updated yet
            IsDeleted = false;
            DeletedAt = null;
        }

        /// <summary>
        /// Update the UpdatedAt timestamp - called internally when entity changes
        /// </summary>
        protected void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;

        /// <summary>
        /// Soft delete the entity (idempotent)
        /// </summary>
        public virtual void Delete()
        {
            if (IsDeleted)
                return; // Already deleted, idempotent operation

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            SetUpdatedAt();
        }

        /// <summary>
        /// Restore a soft-deleted entity (idempotent)
        /// </summary>
        public virtual void Restore()
        {
            if (!IsDeleted)
                return; // Not deleted, nothing to restore

            IsDeleted = false;
            DeletedAt = null;
            SetUpdatedAt();
        }

        /// <summary>
        /// Check if entity is deleted
        /// </summary>
        public bool HasBeenDeleted => IsDeleted;

        /// <summary>
        /// Get days since creation
        /// </summary>
        public int DaysSinceCreation => (int)(DateTime.UtcNow - CreatedAt).TotalDays;

        /// <summary>
        /// Get days since last update (or creation if never updated)
        /// </summary>
        public int DaysSinceLastUpdate
        {
            get
            {
                var lastChange = UpdatedAt ?? CreatedAt;
                return (int)(DateTime.UtcNow - lastChange).TotalDays;
            }
        }
    }
}
