using AutoMapper;
using ArticulosAPI.Dto;
using ArticulosAPI.Modelos;

namespace ArticulosAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ArticuloDto, Articulo>();
            CreateMap<Articulo, ArticuloDto>();
        }
    }
}
