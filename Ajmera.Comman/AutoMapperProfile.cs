using Ajmera.DTO;
using Ajmera.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ajmera.Comman
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BookDTO, Book>();
            CreateMap<Book, BookDTO>();
        }

    }
}
