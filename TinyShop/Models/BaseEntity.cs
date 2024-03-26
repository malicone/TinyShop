using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public abstract class BaseEntity
    {
        public const int LengthSmallExtra = 32;
        public const int LengthSmall = 64;
        public const int LengthMedium = 256;
        public const int LengthLarge = 512;
        public const int LengthLargeExtra = 1024;

        // The default convention creates a primary key column for a property whose name is Id or <Entity Class Name>Id.
        // The Key attribute overrides this default convention.
        // https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx
        public int Id { get; set;}
    }
}
