﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyShop.Data;
using TinyShop.Infrastructure;
using TinyShop.Models;

namespace TinyShop.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        public FilesController( ShopContext context, IWebHostEnvironment appEnvironment )
        {
            _context = context;
            _appEnvironment = appEnvironment;            
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetFileById( int id )
        {
            FileTag foundFile = await _context.FileTags.FirstOrDefaultAsync( f => f.Id == id );
            if ( foundFile != null )
            {
                return File( foundFile.Body, GetContentTypeByExt( foundFile.Ext ) );
            }
            return NotFound();
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetProductMainImage( int productId )
        {
            Product product = await _context.Products.FirstOrDefaultAsync( p => p.Id == productId );
            if ( product != null )
            {
                _context.Entry( product ).Collection( p => p.DescImages ).Load();
                if ( product.MainImage == null )
                {
                    // max 20210203: todo: should be optimized - several requests tries to read the file at a time
                    byte[] image = GetProductNoPhotoImageAsSingleInstance();
                    if ( image != null )
                    {
                        return File( image, GetContentTypeByExt( IOUtils.GetExtensionWithoutDot( _PRODUCT_NO_PHOTO_IMAGE_FILE_NAME ) ) );
                    }
                }
                else
                {
                    return File( product.MainImage.Body, GetContentTypeByExt( product.MainImage.Ext ) );
                }                
            }
            return NotFound();
        }

        private byte[] GetProductNoPhotoImageAsInstance()
        {
            try
            {
                string path = IOUtils.ComposeFilesPath( _appEnvironment.WebRootPath, _PRODUCT_NO_PHOTO_IMAGE_FILE_NAME );                
                using ( var fileStream = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read ) )
                {
                    return fileStream.ToByteArray();
                }
            }
            catch ( Exception )
            {
                return null;
            }
        }
        private byte[] GetProductNoPhotoImageAsSingleInstance()
        {
            if ( _productNoPhotoImage == null )
                _productNoPhotoImage = GetProductNoPhotoImageAsInstance();
            return _productNoPhotoImage;
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
        private const string _PRODUCT_NO_PHOTO_IMAGE_FILE_NAME = "no-photo-254-180.png";
        private static byte[] _productNoPhotoImage;
    }
}