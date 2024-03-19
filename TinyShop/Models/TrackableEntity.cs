using System;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public abstract class TrackableEntity : BaseEntity
    {
        [StringLength( LENGTH_LARGE )]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public void SetCreateStamp( string? createdBy = null )
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
        }

        [StringLength( LENGTH_LARGE )]
        public string? UpdatedBy { get; set; }                
        public DateTime? UpdatedAt { get; set; }
        public void SetUpdateStamp( string? updatedBy = null )
        {
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.Now;
        }
    }
}
