using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
    [Index( nameof( IdExternal ) )]
    public abstract class JsonSupportable : SoftDeletableEntity
    {
        public const int LengthIdExturnal = 1024;

        [StringLength( LengthIdExturnal )]        
        public string? IdExternal { get; set; }
        public string? RawJson { get; set; }        
    }
#nullable disable
}
