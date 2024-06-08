using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
    public abstract class SoftDeletableEntity : TrackableEntity
    {
        public DateTime? SoftDeletedAt { get; set; }
        
        [StringLength( StrLengthLarge )]
        public string? SoftDeletedBy { get; set; }
        
        [NotMapped]
        public bool IsSoftDeleted => SoftDeletedAt.HasValue;

        [NotMapped]
        public bool IsNotSoftDeleted => SoftDeletedAt.HasValue == false;

        public void SoftDelete( string? deletedBy = null )
        {
            SoftDeletedAt = DateTime.Now;
            SoftDeletedBy = deletedBy;
        }
        public void SoftRestore()
        {
            SoftDeletedAt = null;
            SoftDeletedBy = null;
        }
    }
}
