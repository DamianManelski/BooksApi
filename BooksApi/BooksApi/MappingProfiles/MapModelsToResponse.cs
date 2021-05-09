using AutoMapper;

namespace BooksApi.MappingProfiles
{
    public class MapModelsToResponse : Profile
    {
        //TODO: DB data models should not be returned as response. Since every DB model change will affect user experience. 
        //However time for this task is over... :)
    }
}
