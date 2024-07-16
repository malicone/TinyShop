using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Infrastructure
{
    public static class IOExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                stream.CopyTo(memStream);
                return memStream.ToArray();
            }
        }

        public static async Task<byte[]> ToByteArrayAsync(this Stream stream)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                await stream.CopyToAsync(memStream);
                return memStream.ToArray();
            }
        }
    }
}
