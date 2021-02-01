using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Utils
{
    public static class IOUtils
    {
        /// <summary>
        /// Returns files subdirectory name.
        /// </summary>
        public static string FilesDirectory { get { return "Files"; } }
        
        public static string ComposeFilesPath( string webRoot, string fileName )
        {
            return $"{webRoot}/{FilesDirectory}/{fileName}";
        }
    }
}
