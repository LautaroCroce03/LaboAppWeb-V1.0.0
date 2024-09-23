using AutoMapper;
using LaboAppWebV1._0._0.Models; 
using LaboAppWebV1._0._0.ModelsDto;

namespace LaboAppWebV1._0._0.Mappers
{
    public class MappersP : Profile
    {
        public MappersP()
        {


            //Comanda a ComandaDto
            CreateMap<Comanda, ComandaDto>()
                .ForMember(dest => dest.Pedidos, opt => opt.MapFrom(src => src.Pedidos));

            //ComandaDto a Comanda
            CreateMap<ComandaDto, Comanda>()
                .ForMember(dest => dest.Pedidos, opt => opt.MapFrom(src => src.Pedidos));

            //Comanda a ComandaDetalleDto
            CreateMap<Comanda, ComandaDetalleDto>()
                .ForMember(dest => dest.IdComandas, opt => opt.MapFrom(src => src.IdComandas))
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.NombreCliente))
                .ForMember(dest => dest.IdMesa, opt => opt.MapFrom(src => src.IdMesa))
                .ForMember(dest => dest.Mesa, opt => opt.MapFrom(src => src.IdMesaNavigation.Nombre));

            //ComandaDetalleDto a Comanda
            CreateMap<ComandaDetalleDto, Comanda>()
                .ForMember(dest => dest.IdComandas, opt => opt.MapFrom(src => src.IdComandas))
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.NombreCliente))
                .ForMember(dest => dest.IdMesa, opt => opt.MapFrom(src => src.IdMesa));



            //Empleado a EmpleadoDto
            CreateMap<Empleado, EmpleadoDto>();

            //EmpleadoDto a Empleado
            CreateMap<EmpleadoDto, Empleado>();

            //Empleado a EmpleadoListDto
            CreateMap<Empleado, EmpleadoListDto>()
                .ForMember(dest => dest.IdEmpleado, opt => opt.MapFrom(src => src.IdEmpleado));

            //EmpleadoListDto a Empleado
            CreateMap<EmpleadoListDto, Empleado>()
                .ForMember(dest => dest.IdEmpleado, opt => opt.MapFrom(src => src.IdEmpleado));



            // EstadoMesa a EstadoMesaDto
            CreateMap<EstadoMesa, EstadoMesaDto>();
            CreateMap<EstadoMesa, EstadoMesaList>();

            // Mesa a MesaDto
            CreateMap<Mesa, MesaDto>();
            CreateMap<Mesa, MesaListDto>()
                .ForMember(dest => dest.IdMesa, opt => opt.MapFrom(src => src.IdMesa))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.IdEstado, opt => opt.MapFrom(src => src.IdEstado));



            // Pedido a PedidoDto
            CreateMap<Pedido, PedidoDto>();

            // Pedido a PedidoListDto
            CreateMap<Pedido, PedidoListDto>()
                .ForMember(dest => dest.IdPedido, opt => opt.MapFrom(src => src.IdPedido))
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
                .ForMember(dest => dest.ProductoDescripcion, opt => opt.MapFrom(src => src.IdProductoNavigation.Descripcion))
                .ForMember(dest => dest.IdEstadoPedido, opt => opt.MapFrom(src => src.IdEstado))
                .ForMember(dest => dest.EstadoDescripcion, opt => opt.MapFrom(src => src.IdEstadoNavigation.Descripcion))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.FechaFinalizacion, opt => opt.MapFrom(src => src.FechaFinalizacion));



            // Producto a ProductoDto
            CreateMap<Producto, ProductoDto>();

            // ProductoPedidoDto (que incluye Producto y Pedido)
            CreateMap<Producto, ProductoPedidoDto>()
                .ForMember(dest => dest.Producto, opt => opt.MapFrom(src => new ProductoDto
                {
                    IdProducto = src.IdProducto,
                    IdSector = src.IdSector,
                    Descripcion = src.Descripcion,
                    Stock = src.Stock,
                    Precio = src.Precio
                }))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio));



            // Role a RolDto
            CreateMap<Role, RolDto>();

            // Role a RolListDto
            CreateMap<Role, RolListDto>()
                .ForMember(dest => dest.IdRol, opt => opt.MapFrom(src => src.IdRol))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));


            // Sectore a SectorDto
            CreateMap<Sectore, SectorDto>();

            // Sectore a SectorListDto
            CreateMap<Sectore, SectorListDto>()
                .ForMember(dest => dest.IdSector, opt => opt.MapFrom(src => src.IdSector))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion));
        }
    }
}