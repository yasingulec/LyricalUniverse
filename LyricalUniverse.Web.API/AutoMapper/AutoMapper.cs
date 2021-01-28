using AutoMapper;
using LyricalUniverse.Entities;
using LyricalUniverse.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.AutoMapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Album, AlbumModel>();
        }
    }
}
