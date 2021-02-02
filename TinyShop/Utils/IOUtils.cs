using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Utils
{
    public static class IOUtils
    {
        /// <summary>
        /// Returns files subdirectory name (directory where files are stored).
        /// </summary>
        public static string FilesDirectory { get { return "Files"; } }
        
        public static string ComposeFilesPath( string webRoot, string fileName )
        {
            return $"{webRoot}/{FilesDirectory}/{fileName}";
        }

        public static string GetExtensionWithoutDot( string fileName )
        {
            string ext = Path.GetExtension( fileName );
            if ( ext.Length > 0 )
            {
                if ( ext[ 0 ].Equals( '.' ) )
                    return ext.Remove( 0, 1 );
            }
            return ext;
        }
    }
}
