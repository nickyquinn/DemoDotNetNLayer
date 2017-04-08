using AutoMapper;
using DemoApp.Data.Entities;
using DemoApp.Services.DTO;

namespace DemoApp.Services
{
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Configure the auto mapper for data transfer objects
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //For the maps, manually wire up the renamed Dto/non-Dto fields
                cfg.CreateMap<PersonDto, Person>()
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonDtoId));
                cfg.CreateMap<Person, PersonDto>()
                    .ForMember(dest => dest.PersonDtoId, opt => opt.MapFrom(src => src.PersonId));
            });
        }
    }
}
