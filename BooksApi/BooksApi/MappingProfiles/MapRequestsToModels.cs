using AutoMapper;
using BooksApi.Controllers;
using BooksApi.Controllers.Requests;
using BooksApi.Controllers.RequestsResponse;
using BooksApi.Infrastructure;
using BooksApi.Infrastructure.Models;

namespace BooksApi.MappingProfiles
{
    public class MapRequestsToModels : Profile
    {

        public MapRequestsToModels()
        {
            CreateMap<BookRequest, Book>().ForMember(s => s.Id, opt => opt.Ignore()).ForMember(s => s.DeletionDate, opt => opt.Ignore());
            CreateMap<UserRequest, User>().ForMember(s => s.Id, opt => opt.Ignore());
            CreateMap<UserBookOpinionRequest, UserBookOpinion>().ForMember(s => s.Id, opt => opt.Ignore());
            CreateMap<PagingParamsRequest, PagingParameters>();
        }
    }
}
