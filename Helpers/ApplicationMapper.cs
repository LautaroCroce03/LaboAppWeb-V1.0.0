using AutoMapper;

namespace LaboAppWebV1._0._0.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<ModelsDto.PedidoDto, Models.Pedido>().ReverseMap();

            //Automapper context Items
            CreateMap<ModelsDto.PedidoDto, Models.Pedido>()
                    .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom((src, dest, destMember, context) => (DateTime)context.Items["fechaActual"]))
                    .ForMember(dest => dest.FechaFinalizacion, opt => opt.MapFrom((src, dest, destMember, context) => (DateTime)context.Items["fechaActual"]))
                    .ForMember(dest => dest.IdComanda, opt => opt.MapFrom((src, dest, destMember, context) => (int)context.Items["idComanda"]))
                    .ReverseMap();

        }
    }
}
