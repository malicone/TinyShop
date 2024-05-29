using System;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
    public abstract class TrackableEntity : BaseEntity
    {
        [StringLength( StrLengthLarge )]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public void SetCreateStamp( string? createdBy = null )
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
        }

        [StringLength( StrLengthLarge )]
        public string? UpdatedBy { get; set; }                
        public DateTime? UpdatedAt { get; set; }
        public void SetUpdateStamp( string? updatedBy = null )
        {
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.Now;
        }
    }
#nullable disable
}
