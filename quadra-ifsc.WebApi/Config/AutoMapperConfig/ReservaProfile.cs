using AutoMapper;
using eAgenda.Webapi.Config.AutoMapperConfig;
using quadra_ifsc.Dominio.ModuloReserva;
using quadra_ifsc.WebApi.ViewModels.ModuloReserva;

namespace quadra_ifsc.WebApi.Config.AutoMapperConfig
{
    public class ReservaProfile : Profile
    {
        public ReservaProfile()
        {
            CreateMap<FormsReservaViewModel, Reserva>()
                .ForMember(destino => destino.UsuarioId, opt => opt.MapFrom<UsuarioResolver>())
                .ForMember(destino => destino.Id, opt => opt.Ignore());

            CreateMap<Reserva, ListarReservaViewModel>();

            CreateMap<Reserva, VisualizarReservaViewModel>();

            CreateMap<Reserva, FormsReservaViewModel>();
        }        
    }
}