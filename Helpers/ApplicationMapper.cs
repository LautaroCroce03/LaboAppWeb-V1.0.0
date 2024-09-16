using AutoMapper;
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<ModelsDto.PedidoDto, Models.Pedido>().ReverseMap();
            CreateMap<ModelsDto.EmpleadoDto, Models.Empleado>().ReverseMap();
            CreateMap<ModelsDto.EstadoMesaDto, Models.EstadoMesa>().ReverseMap();

            //Automapper context Items
            CreateMap<ModelsDto.PedidoDto, Models.Pedido>()
                    .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom((src, dest, destMember, context) => (DateTime)context.Items["fechaActual"]))
                    .ForMember(dest => dest.FechaFinalizacion, opt => opt.MapFrom((src, dest, destMember, context) => (DateTime)context.Items["fechaActual"]))
                    .ForMember(dest => dest.IdComanda, opt => opt.MapFrom((src, dest, destMember, context) => (int)context.Items["idComanda"]))
                    .ReverseMap();

            CreateMap<Models.EstadoMesa, ModelsDto.EstadoMesaList>()
                .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.IdEstado)) // Mapea el IdEstado
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion)); // Mapea la Descripción



        }
    }
}
