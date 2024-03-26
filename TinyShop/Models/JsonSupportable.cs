using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    [Index( nameof( IdExternal ) )]
    public abstract class JsonSupportable : SoftDeletableEntity
    {
        public const int LENGTH_ID_EXTERNAL = 1024;

        [StringLength( LENGTH_ID_EXTERNAL )]        
        public string? IdExternal { get; set; }
        public string? RawJson { get; set; }        
    }
}
