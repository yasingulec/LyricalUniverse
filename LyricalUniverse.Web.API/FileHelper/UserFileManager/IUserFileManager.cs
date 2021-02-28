using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.FileHelper.UserFileManager
{
   public interface IUserFileManager
    {
        FileStream imageStream(string image);
        string SaveImage(IFormFile image);
        bool RemoveImage(string image);
    }
}
