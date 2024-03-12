using System;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class TrackableEntity : BaseEntity
    {
        [StringLength( 512 )]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public void SetCreateStamp( string? createdBy = null )
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
        }

        [StringLength( 512 )]
        public string? UpdatedBy { get; set; }                
        public DateTime? UpdatedAt { get; set; }
        public void SetUpdateStamp( string? updatedBy = null )
        {
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.Now;
        }
    }
}
