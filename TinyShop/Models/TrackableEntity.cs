using System;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class TrackableEntity : BaseEntity
    {
        [StringLength( 512 )]
        public string? CreatedBy { get; set; }
        
        [StringLength( 512 )]
        public string? UpdatedBy { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
