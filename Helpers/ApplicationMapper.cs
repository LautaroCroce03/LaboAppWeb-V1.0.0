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
            CreateMap<ModelsDto.MesaDto, Models.Mesa>().ReverseMap();

            CreateMap<Models.Mesa, ModelsDto.MesaListDto>().ReverseMap();
            CreateMap<Models.EstadoMesa, ModelsDto.EstadoMesaList>();
            CreateMap<ModelsDto.RolDto, Models.Role>();
            CreateMap<Models.Role, ModelsDto.RolListDto>();
            CreateMap<ModelsDto.SectorDto, Models.Sectore>();
            CreateMap<Models.Sectore, ModelsDto.SectorListDto>();

            //Automapper context Items
            CreateMap<ModelsDto.PedidoDto, Models.Pedido>()
                    .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom((src, dest, destMember, context) => (DateTime)context.Items["fechaActual"]))
                    //.ForMember(dest => dest.FechaFinalizacion, opt => opt.MapFrom((src, dest, destMember, context) => (DateTime)context.Items["fechaActual"]))
                    .ForMember(dest => dest.IdComanda, opt => opt.MapFrom((src, dest, destMember, context) => (int)context.Items["idComanda"]))
                    .ReverseMap();

            CreateMap<Models.EstadoPedido, ModelsDto.EstadoPedidoDto>();

            CreateMap<Models.Pedido, ClienteResponseDto>()
                           .ForMember(dest => dest.TiempoEstimado, opt => opt.MapFrom(src => src.TiempoEstimado))
                           .ForMember(dest => dest.TiempoDemorado, opt => opt.MapFrom(src => (int)Math.Round((DateTime.Now - src.FechaCreacion).TotalMinutes - src.TiempoEstimado, 0)));

            //CreateMap<Models.EstadoMesa, ModelsDto.EstadoMesaList>()
            //    .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.IdEstado)) 
            //    .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));

            //CreateMap<Models.Mesa, MesaListDto>()
            //    .ForMember(dest => dest.IdMesa, opt => opt.MapFrom(src => src.IdMesa))
            //    .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.IdEstado))
            //    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

        }
    }
}
