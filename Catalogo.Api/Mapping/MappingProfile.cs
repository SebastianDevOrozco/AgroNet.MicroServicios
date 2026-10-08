using AutoMapper;
using Catalogo.Api.Dtos.Cosechas;
using Catalogo.Api.Models;

namespace Catalogo.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //COSECHAS

            //Mapeo CosechaCreateDto --> Cosecha
            CreateMap<CosechaCreateDto, Cosecha>();
            //Mapeo Cosecha --> CosechaReadDto
            CreateMap<Cosecha, CosechaReadDto>()
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom<NombreUsuarioResolver>()); 



        }
    }
}
