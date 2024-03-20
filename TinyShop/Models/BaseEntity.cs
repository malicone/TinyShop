using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public abstract class BaseEntity
    {
        public const int LENGTH_SMALL_EXTRA = 32;
        public const int LENGTH_SMALL = 64;
        public const int LENGTH_MEDIUM = 256;
        public const int LENGTH_LARGE = 512;
        public const int LENGTH_LARGE_EXTRA = 1024;

        // The default convention creates a primary key column for a property whose name is Id or <Entity Class Name>Id.
        // The Key attribute overrides this default convention.
        // https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx
        public int Id { get; set;}
    }
}
