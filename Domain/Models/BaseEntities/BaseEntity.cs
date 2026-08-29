using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.BaseEntities
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }




        /// <summary>
        /// Soft delete the entity
        /// </summary>
        public virtual void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Restore a soft-deleted entity
        /// </summary>
        public virtual void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
