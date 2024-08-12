using AutoMapper;
using ShopAppDll.Entities;
using ShopAppPB301Practice.DTOs.BookDTOs;
using ShopAppPB301Practice.DTOs.GroupDTOs;
using ShopAppPB301Practice.DTOs.StudentDTOs;
using ShopAppPB301Practice.Entities;
using ShopAppPB301Practice.Extensions;

namespace ShopAppPB301Practice.Profiles
{
    public class MapProfile : Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MapProfile(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var uriBuilder = new UriBuilder(
                _httpContextAccessor.HttpContext.Request.Scheme,
                _httpContextAccessor.HttpContext.Request.Host.Host,
                (int)_httpContextAccessor.HttpContext.Request.Host.Port
                );
            var url = uriBuilder.Uri.AbsoluteUri;
            CreateMap<Group, GroupReturnDTO>()
                .ForMember(d => d.Image, map => map.MapFrom(s => url + "wwwroot/uploads/images" + s.Image));
            CreateMap<Student, StudentInGroupReturnDTO>();
            CreateMap<GroupCreateDTO, Group>()
                .ForMember(d => d.Image, map => map.MapFrom(dto => dto.File.Save(Directory.GetCurrentDirectory(), "wwwroot/uploads/images")));
            //Student Maps
            CreateMap<StudentCreateDTO, Student>().ReverseMap();
            CreateMap<Student, StudentReturnDTO>().ReverseMap();
            CreateMap<Group, GroupInStudentReturnDTO>();
            //Book Maps
            CreateMap<BookCreateDTO, Book>();
            CreateMap<Book, BookReturnDTO>();
            CreateMap<BookAuthor,AuthorInBookReturnDTO>();
            CreateMap<Author,AuthorInBookReturnDTO>();
        }
    }
}
