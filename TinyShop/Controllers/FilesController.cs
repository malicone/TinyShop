using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Models;

namespace TinyShop.Controllers
{
    public class FilesController : Controller
    {
        public FilesController( ShopContext context, IWebHostEnvironment appEnvironment )
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> GetFileById( int id )
        {
            FileTag foundFile = await _context.FileTags.FirstOrDefaultAsync( f => f.Id == id );
            if ( foundFile != null )
            {
                return File( foundFile.Body, GetContentTypeByExt( foundFile.Ext ) );
            }
            return NotFound();
        }

        private static string GetContentTypeByExt( string fileExtension )
        {
            fileExtension = fileExtension.ToLower();
            if (fileExtension.Length > 0)// remove dot if exists
            {
                if ( fileExtension[ 0 ].Equals( '.' ) )
                    fileExtension = fileExtension.Remove( 0, 1 );
            }
            // max 20210201: https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
            switch ( fileExtension )
            {
                case "png":
                case "jpg":
                case "jpeg":
                case "gif":
                case "bmp":
                case "webp":
                    return $"image/{fileExtension}";
                case "txt":
                    return "text/plain";
                case "pdf": 
                    return "application/pdf";
                default:// we dont't know what is it
                    return "application/octet-stream";                    
            }
        }

        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
    }
}
