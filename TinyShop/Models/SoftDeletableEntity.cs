using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
    public class SoftDeletableEntity : TrackableEntity
    {
        public DateTime? SoftDeletedAt { get; set; }
        
        [StringLength( 512 )]
        public string? SoftDeletedBy { get; set; }
        
        [NotMapped]
        public bool IsSoftDeleted => SoftDeletedAt.HasValue;
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
